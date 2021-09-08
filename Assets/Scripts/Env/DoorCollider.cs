using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] private List<Door> doors;
    void Start()
    {
        transform.tag = "Door";
    }
    public void OnShoot(Transform performer){
        foreach(Door door in doors){
            if(door.canBeShot && door.HasKey(performer) && door.CanOpen()){
                door.StartCoroutine(door.OpenDoor());
            }
        }
    }
    public void OnUse(Transform performer){
        foreach(Door door in doors){
            if(door.HasKey(performer) && door.CanOpen()){
                door.StartCoroutine(door.OpenDoor());
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        foreach(Door door in doors){
            if(!door.useOnly && other.CompareTag("Player")){
                if(door.CanOpen() && door.HasKey(other.transform)){
                    door.StartCoroutine(door.OpenDoor());
                }
            }
        }
    }
}
