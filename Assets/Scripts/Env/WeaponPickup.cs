using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponType weaponIndex;
    public string pickupText;
    public AudioClip pickupSound;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if(!PlayerBase.instance.weapons.GetComponent<WeaponManager>().weaponInventory[(int)weaponIndex]){
                PlayerBase.instance.weapons.GetComponent<WeaponManager>().weaponInventory[(int)weaponIndex] = true;
                if(PlayerBase.instance.weapons.GetComponent<WeaponManager>().switchNewWeapon) PlayerBase.instance.weapons.GetComponent<WeaponManager>().desiredWeaponIndex = (int) weaponIndex; 
                PlayerBase.instance.weapons.GetComponent<WeaponManager>().UpdateInventory();
                other.GetComponent<AudioSource>().PlayOneShot(pickupSound);
                MessageDisplay.DisplayMessage(pickupText);
                Destroy(gameObject);
            }
        }
    }
}
