using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.0f;
    public float runningSpeed = 12f;
    public float crouchSpeed = 5.5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float ascendSpeed = 2f;
    public Vector3 moveDirection = Vector3.zero;
    public CharacterController characterController;

    public bool isRunning = false, isJumping = false, isSubmerged = false;
    public bool canMove = true;
    public List<AudioClip> footSteps;
    public List<AudioClip> jumpSounds;
    public float stepTime;
    private AudioSource _source;
    private float _stepTimer;
    private void Start() {
        characterController = GetComponent<CharacterController>();
        _source = GetComponent<AudioSource>();    
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float speedFB = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float speedSTS = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY  = moveDirection.y;
        moveDirection = (forward * speedFB) + (right * speedSTS);
        if (Input.GetButton("Jump") && canMove && (characterController.isGrounded || isSubmerged)){
            moveDirection.y = (!isSubmerged ? jumpSpeed : ascendSpeed);
            if(!isSubmerged) SoundJump();
        } else {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded && !isSubmerged)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (!characterController.isGrounded && isSubmerged){
            moveDirection.y -= ascendSpeed * Time.deltaTime;
        }
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        FootstepUpdate();
    }
    public void FootstepUpdate(){
        if(IsPlayerMoving() && characterController.isGrounded){
            _stepTimer += Time.deltaTime;
            if(_stepTimer >= (isRunning ? (stepTime * (walkingSpeed / runningSpeed)) : stepTime)){
                _source.PlayOneShot(footSteps[Random.Range(0,footSteps.Count-1)]);
                _stepTimer = 0;
            }
        } else _stepTimer = 0;
    }
    public void SoundJump(){
        _source.PlayOneShot(jumpSounds[Random.Range(0,jumpSounds.Count-1)]);
        _source.PlayOneShot(footSteps[Random.Range(0,footSteps.Count-1)]);
    }
    public bool IsPlayerMoving(){
        return (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f);
    }
}
