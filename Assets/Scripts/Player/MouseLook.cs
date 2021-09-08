using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public float lookXLimit = 90f;
    public Transform playerBody;   
    float xRotation = 0f;
    private Camera _camera;

    // Update is called once per frame
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.rotation *= Quaternion.Euler(0f, mouseX ,0f);
    }
}
