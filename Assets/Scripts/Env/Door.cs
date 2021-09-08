using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    public float doorMoveSpeed;
    public float doorRotateSpeed;
    public float openStateTime;
    public Transform openPosition;
    public bool _open = false;
    public bool useOnly, canBeShot, needsKey, keepHeld;
    public KeyColor key;
    public AudioClip openSound, closeSound, slamSound;
    private AudioSource _source;
    private bool _opening, _closing, _holding;
    Vector3 closePosition;
    Quaternion closeRotation;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.spatialBlend = 1;
        closePosition = transform.localPosition;
        closeRotation = transform.localRotation;
    }

    void Update() {
            if(openStateTime > 0 & CanClose()){
                StartCoroutine(DoorHold());
            }
        }    
    private IEnumerator DoorHold(){
        _holding = true;
        float timer = 0;
        while(timer < openStateTime){
            timer += Time.deltaTime;
            yield return null;
        }
        _holding = false;
        StartCoroutine(CloseDoor());
    }
    public IEnumerator OpenDoor(){
        _opening = true;
        _source.loop = true;
        _source.clip = openSound;
        _source.Play();
        while((transform.localPosition != openPosition.localPosition || transform.localRotation != openPosition.localRotation)){
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, openPosition.localRotation, Time.deltaTime * doorRotateSpeed);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, openPosition.localPosition, Time.deltaTime * doorMoveSpeed);
            yield return null;
        }
        _source.Stop();
        _source.loop = false;
        _source.clip = slamSound;
        _source.Play();
        _opening = false;
        _open = true;
    }
    public IEnumerator CloseDoor(){
        _closing = true;
        _source.loop = true;
        _source.clip = closeSound;
        _source.Play();
        while((transform.localPosition != closePosition || transform.localRotation != closeRotation)){
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, closeRotation, Time.deltaTime * doorRotateSpeed);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, closePosition, Time.deltaTime * doorMoveSpeed);
            yield return null;
        }
        _source.Stop();
        _source.loop = false;
        _source.clip = slamSound;
        _source.Play();
        _closing = false;
        _open = false;
    }
    private bool CanInteract(){
        return !_closing && !_opening && !_holding;
    }
    public bool CanOpen(){
        return CanInteract() && !_open;
    }
    private bool CanClose(){
        return CanInteract() && _open;
    }
    public bool HasKey(Transform performer){
        if(needsKey){
            if(performer.GetComponent<KeyManager>().keyInventory[(int) key]){
                return true;
            } else return false;
        } else {
            return true;
        }
    }
}
