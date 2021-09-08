using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header ("Bob & Sway")]
    public float vertBobbingAmount = 12f;
    public float vertBobbingSpeed = 14f;
    public float horBobbingAmount = 12f;
    public float horBobbingSpeed = 14f;
    public float horSwayAmount = 14f;
    public float horSwaySpeed = 22f;
    public float jumpBumpAmount = 22f;
    public float jumpBumpSpeed = 14f;
    public float recoilAmount;
    public float recoilSpeed;
    private Weapon _weapon;
    private PlayerMovement _player;
    private float defaultVerticalPos, defaultHorizontalPos,
    modVerticalPos, modHorizontalPos,
    swayHorizontalPos, jumpVerticalPos, shotHorizontalPos, shotVerticalPos;
    private float vertTimer = 0;
    private float horTimer = 0;
    private float swayTimer = 0;
    private float jumpTimer = 0;
    private float shotTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultVerticalPos = transform.localPosition.y;
        defaultHorizontalPos = transform.localPosition.x;
        _player = transform.parent.parent.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        modHorizontalPos = defaultHorizontalPos + shotHorizontalPos + swayHorizontalPos;
        modVerticalPos = defaultVerticalPos + jumpVerticalPos + shotVerticalPos;
        Sway();
        if(_player.characterController.isGrounded){
            if(IsMoving()){
                //Player is moving
                LandUpdate();
                MoveUpdate();
            }
            else {
            //Idle
            LandUpdate();
            MoveToOrigin();
            SwayUpdate();
            //transform.localPosition = new Vector3(transform.localPosition.x, defaultVerticalPos ,transform.localPosition.z);
            }
        } else {
            JumpBump();
            transform.localPosition = new Vector3(modHorizontalPos, transform.localPosition.y, transform.localPosition.z);
        }
    }
    bool IsMoving(){
        return Mathf.Abs(_player.moveDirection.x) > 0.1f || Mathf.Abs(_player.moveDirection.z) > 0.1f;
    }
    void MoveUpdate(){
        vertTimer += Time.deltaTime * vertBobbingSpeed;
        horTimer += Time.deltaTime * horBobbingSpeed;
        transform.localPosition = new Vector3(transform.localPosition.x, modVerticalPos + Mathf.Sin(vertTimer) * vertBobbingAmount, transform.localPosition.z);
        transform.localPosition = new Vector3(modHorizontalPos + Mathf.Sin(horTimer) * horBobbingAmount, transform.localPosition.y, transform.localPosition.z);
    }
    void MoveToOrigin(){
        vertTimer = 0;
        horTimer = 0;
        transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, modHorizontalPos, Time.deltaTime * horBobbingSpeed),
        Mathf.Lerp(transform.localPosition.y, modVerticalPos, Time.deltaTime * jumpBumpSpeed),
        transform.localPosition.z);
    }
    void Sway(){
        swayTimer += Time.deltaTime * horSwaySpeed;
        swayHorizontalPos = Mathf.Atan(swayTimer) * horSwayAmount * Input.GetAxis("Horizontal");
    }
    void SwayUpdate(){
            swayTimer = 0;
            swayHorizontalPos = Mathf.Lerp(swayHorizontalPos, 0, Time.deltaTime * horSwaySpeed);
    }
    void JumpBump(){
        jumpTimer += Time.deltaTime * jumpBumpSpeed;
        jumpVerticalPos = -Mathf.Atan(jumpTimer)*jumpBumpAmount;
        transform.localPosition = new Vector3(transform.localPosition.x, modVerticalPos, transform.localPosition.z);
    }
    void LandUpdate(){
            jumpTimer = 0;
            //modVerticalPos = -Mathf.Atan(jumpTimer)*jumpBumpAmount;
            jumpVerticalPos = Mathf.Lerp(jumpVerticalPos, defaultVerticalPos, Time.deltaTime * jumpBumpSpeed);
    }
    public void ShotBump(){
        shotTimer += Time.deltaTime * recoilSpeed;
        shotVerticalPos = -Mathf.Atan(shotTimer) * recoilAmount;
        shotHorizontalPos = Mathf.Atan(shotTimer) * recoilAmount;
    }
    public void ShotUpdate(){
        shotTimer = 0;
        shotVerticalPos = Mathf.Lerp(shotVerticalPos, 0, Time.deltaTime * (recoilSpeed / 10));
        shotHorizontalPos = Mathf.Lerp(shotHorizontalPos, 0, Time.deltaTime * (recoilSpeed / 10));
    }
}