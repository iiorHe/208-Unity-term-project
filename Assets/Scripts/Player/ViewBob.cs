using UnityEngine;

public class ViewBob : MonoBehaviour
{
    public float bobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public PlayerMovement player;

    float defaultViewHeight = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultViewHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.characterController.isGrounded){
            if(Mathf.Abs(player.moveDirection.x) > 0.1f || Mathf.Abs(player.moveDirection.z) > 0.1f){
                //Player is moving
                timer += Time.deltaTime * bobbingSpeed;
                transform.localPosition = new Vector3(transform.localPosition.x, defaultViewHeight + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
            }
            else {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultViewHeight, Time.deltaTime * bobbingSpeed), transform.localPosition.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, defaultViewHeight ,transform.localPosition.z);
            }
        } else {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultViewHeight, Time.deltaTime * bobbingSpeed), transform.localPosition.z);
        }
    }
}
