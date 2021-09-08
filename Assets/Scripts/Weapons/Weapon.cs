using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [Header ("Base")]
    [SerializeField] private int damage;
    [SerializeField] private int projNum = 1;
    [SerializeField] private float range;
    [SerializeField] private float spread = 0;

    [Header ("Bools")]
    [SerializeField] private bool isAuto; 
    //[SerializeField] private bool reloadInterrupt = false;
    [SerializeField] private bool hasClip;
    [SerializeField] private bool isInfinite;

    [Header ("Ammo")]
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int clipSize;
    private int currentClip;
    [SerializeField] private int ammoUsedPerShot;
    [SerializeField] private int reloadAmount;

    [Header ("Speed")]
    [SerializeField] private float speedMultiplier = 1;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireOffset = 0;
    [SerializeField] private float reloadBeginTime;
    [SerializeField] private float reloadActionTime;
    [SerializeField] private float reloadEndTime;
    
    [Header ("Inventory")]
    [SerializeField] private int weaponSlot;
    [SerializeField] private int subSlot;
    [Header ("Visual")]
    [SerializeField] private Camera _playerCam;
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private GameObject bloodImpact;
    [SerializeField] private LineRenderer bulletTrail;
    [SerializeField] private TMPro.TMP_Text uiText;
    private int _rayMask = ~((1 << 6) | (1 << 2));
    private Animator _weaponAnim;
    private AmmoData _ammoData;
    private WeaponSway _sway;
    private string _ammoDisplayText;
    private bool _reloading, _shooting;
    private void OnEnable() {
        _reloading = false;
        _shooting = false;
    }
    private void OnDisable() {
        StopAllCoroutines();
    }
    
    void Start(){
        _weaponAnim = GetComponent<Animator>();
        _sway = GetComponent<WeaponSway>();
        _ammoData = transform.parent.parent.GetComponent<AmmoData>();
        _playerCam = transform.parent.parent.GetComponent<PlayerBase>().playerCamera;
        currentClip = clipSize;
        UpdateUIText();
    }

    // Update is called once per frame
    void Update(){
        if(!_shooting) _sway.ShotUpdate();
        if((isAuto ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1")) && CanFire()){
            StartCoroutine(Shoot());
        }
        if(Input.GetButtonDown("Fire2") && CanReload()){
            StartCoroutine(Reload());
        }
        uiText.text = _ammoDisplayText;
        UpdateUIText();
    }
    IEnumerator Shoot(){
        _shooting = true;
        _weaponAnim.SetTrigger("Shoot");
        _sway.ShotBump();
        yield return new WaitForSeconds(fireOffset);
        for(int i = 0; i < projNum; i++){
            RaycastHit hit;
            Vector3 fireDir = FireDirection();
            if(Physics.Raycast(_playerCam.transform.position, fireDir, out hit, range, _rayMask)){
                Debug.Log(hit.transform.name);
                SpawnBulletTrail(hit.point);
                HitAction(hit);
            } else {
                Debug.Log("Hit Nothing");
                SpawnBulletTrail(_playerCam.transform.position + fireDir * range);
            }
        }
        currentClip -= ammoUsedPerShot;
        UpdateUIText();
        yield return new WaitForSeconds(Mathf.Abs(fireRate - fireOffset));
        _shooting = false;
    }
    IEnumerator Reload(){
        _reloading = true;
        _weaponAnim.SetTrigger("loadBegin");
        yield return new WaitForSeconds(reloadBeginTime);
        while(!Reloaded()){
            _weaponAnim.SetTrigger("loadAction");
            yield return new WaitForSeconds(reloadActionTime);
            AmmoUpdate();
        }
        _weaponAnim.SetTrigger("loadEnd");
        yield return new WaitForSeconds(reloadEndTime);
        _reloading = false;
    }
    private void HitAction(RaycastHit hit){
        switch(hit.transform.tag){
            case "Door":
                Debug.Log(hit.transform.tag);
                if(hit.transform.TryGetComponent<DoorCollider>(out DoorCollider door)){
                    door.OnShoot(transform.parent.parent.transform);
                    if(bulletImpact){
                        GameObject impactEffect = Instantiate(bulletImpact, hit.point, _playerCam.transform.rotation);
                        Destroy(impactEffect, 1.2f);
                    }
                }
                break;
            case "Destructable":
                Debug.Log(hit.transform.tag);
                if(hit.transform.TryGetComponent<EntityHealth>(out EntityHealth destructable)){
                    destructable.TakeDamage(damage);
                    if(bulletImpact){
                        GameObject impactEffect = Instantiate(bulletImpact, hit.point, _playerCam.transform.rotation);
                        Destroy(impactEffect, 1.2f);
                    }
                }
                break;
            case "Enemy":
                Debug.Log(hit.transform.tag);
                if(hit.transform.TryGetComponent<EntityHealth>(out EntityHealth enemy)){
                    enemy.TakeDamage(damage);
                    if(bloodImpact){
                        GameObject impactEffect = Instantiate(bloodImpact, hit.point, _playerCam.transform.rotation);
                        Destroy(impactEffect, 1.2f);
                    }
                }
                break;
            default:
                if(bulletImpact){
                    GameObject impactEffect = Instantiate(bulletImpact, hit.point, _playerCam.transform.rotation);
                    Destroy(impactEffect, 1.2f);
                }
                break;
        }
    }
    /* 
        Method for both adding ammo to the current weapon, and deducting ammo from the AmmoData class.
        If reloadAmount exceeds the amount the clip can hold, the required amount to reach clipsize is used
        If the remaining ammo is less than the reload amount, the remaining ammo gets added to the clip, and
        ammo data is set to zero 
    */
    private void AmmoUpdate(){
        int ammoReloaded = 0;
        int ammoInReserve = _ammoData.ammoInventory[(int)ammoType].currentAmmo;
        if(!isInfinite){
            if(ammoInReserve >= reloadAmount){
                if(currentClip + reloadAmount <= clipSize){
                    ammoReloaded = reloadAmount;
                    currentClip += ammoReloaded;
                } else {
                ammoReloaded = clipSize - currentClip;
                currentClip += ammoReloaded;
                }
            } else {
                if(currentClip + ammoInReserve <= clipSize){
                    ammoReloaded = ammoInReserve;
                    currentClip += ammoReloaded;
                } else {
                    ammoReloaded = clipSize - currentClip;
                    currentClip += ammoReloaded;
                }
            }
        } else {
            if(currentClip + reloadAmount <= clipSize){
                currentClip += reloadAmount;
            } else {
                currentClip += clipSize - currentClip;;
            }
        }
        _ammoData.ammoInventory[(int)ammoType].ModAmmo(ammoInReserve - ammoReloaded);
        UpdateUIText();
    }
    public void UpdateUIText(){
        string reserveText, clipText;
        if(hasClip){
        clipText = currentClip.ToString();
        } else {
            clipText = "-";
        }
        if(!isInfinite){
            reserveText = _ammoData.ammoInventory[(int)ammoType].currentAmmo.ToString();
        } else {
            reserveText = "-";
        }
        _ammoDisplayText = $"{clipText}/{reserveText}";
    }
    private void SpawnBulletTrail(Vector3 target){
        if(bulletTrail){
            GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, _playerCam.transform.position, Quaternion.identity);
            LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();
            lineR.SetPosition(0, _playerCam.transform.position);
            lineR.SetPosition(1, target);
            Destroy(bulletTrailEffect, 0.5f);
        }
    }
    private bool Reloaded(){
        bool clipIsFull = clipSize == currentClip;
        bool ammoIsSpent = _ammoData.ammoInventory[(int)ammoType].currentAmmo <= 0;
        return clipIsFull || ammoIsSpent;
    }
    private bool CanFire(){
        bool hasAmmoInClip = currentClip - ammoUsedPerShot >= 0 || hasClip == false;
        bool isPaused = Time.timeScale == 0f;
        return hasAmmoInClip && !_reloading && !_shooting && !isPaused;
    }
    private bool CanReload(){
        bool isClipFull = currentClip == clipSize;
        bool isPaused = Time.timeScale == 0f;
        bool isEnoughToFire = _ammoData.ammoInventory[(int)ammoType].currentAmmo >= ammoUsedPerShot;
        return !isClipFull && isEnoughToFire && !_reloading && hasClip && !isPaused;
    }
    Vector3 FireDirection(){
        Vector3 targetPos = _playerCam.transform.position + _playerCam.transform.forward * range;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-spread, spread),
            targetPos.y + Random.Range(-spread, spread),
            targetPos.z + Random.Range(-spread, spread)
        );
        Vector3 direction = targetPos - _playerCam.transform.position;
        return  direction.normalized;
    }
}
