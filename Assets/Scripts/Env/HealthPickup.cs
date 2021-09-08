using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public string pickupText;
    public int amount;
    public AudioClip pickupSound;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if(other.GetComponent<PlayerBase>().currentHealth < 100){
                other.GetComponent<PlayerBase>().TakeHealing(amount);
                other.GetComponent<AudioSource>().PlayOneShot(pickupSound);
                MessageDisplay.DisplayMessage(pickupText);
                Destroy(gameObject);
            }
        }
    }
}
