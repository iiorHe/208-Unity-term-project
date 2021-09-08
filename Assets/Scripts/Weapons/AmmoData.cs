using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AmmoData : MonoBehaviour
{
    public static AmmoData instance;   
    public Ammo[] ammoInventory = new Ammo[System.Enum.GetValues(typeof(AmmoType)).Length];
    private void Awake() {
        instance = this;
    }
}
[System.Serializable]
public class Ammo{
        public AmmoType type;
        public int maxAmmo;
        public int currentAmmo;
        
        public void ModAmmo(int input){
            currentAmmo = input;
        }
}
public enum AmmoType{
    Pistol,
    Shell,
    Rifle,
    Explosive,
    None
}