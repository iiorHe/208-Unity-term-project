using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void Start() {
        gameObject.layer = 2;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if(other.TryGetComponent<PlayerMovement>(out var player)){
                player.moveDirection.y = -player.ascendSpeed;
                player.isSubmerged = true;
            }
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            if(other.TryGetComponent<PlayerMovement>(out var player)){
                player.isSubmerged = false;
            }
        }
    }
}
