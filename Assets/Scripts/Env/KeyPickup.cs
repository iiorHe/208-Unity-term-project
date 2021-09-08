using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyColor key;
    public AudioClip pickupSound;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<KeyManager>().keyInventory[(int) key] = true;
            KeyDisplay.UpdateKeyUI((int)key);
            other.GetComponent<AudioSource>().PlayOneShot(pickupSound);
            MessageDisplay.DisplayMessage($"Got the {key} key!");
            Destroy(gameObject);
        }
    }
}
