using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public string pickupText;
    public AmmoType ammoType;
    public int amount;
    public AudioClip pickupSound;
    private AmmoData _ammoData;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if(other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].currentAmmo < other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].maxAmmo){
                if(other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].currentAmmo + amount <= other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].maxAmmo){
                    other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].ModAmmo(other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].currentAmmo + amount);
                } else { 
                    other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].ModAmmo(other.GetComponent<AmmoData>().ammoInventory[(int)ammoType].maxAmmo);
                }
                other.GetComponent<AudioSource>().PlayOneShot(pickupSound);
                AmmoInventoryDisplay.UpdateAmmoDataUI();
                MessageDisplay.DisplayMessage(pickupText);
                Destroy(gameObject);
            }
        }
        
    }
}
