using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoInventoryDisplay : MonoBehaviour
{
    public static AmmoInventoryDisplay instance;
    private TMPro.TMP_Text ammoText;
    private void Awake() {
    instance = this;
    }
    void Start()
    {
        ammoText = transform.GetComponent<TMPro.TMP_Text>();
        AmmoData.instance = PlayerBase.instance.GetComponent<AmmoData>();
        UpdateAmmoDataUI();
    }
    public static void UpdateAmmoDataUI(){
        instance.ammoText.text = AmmoData.instance.ammoInventory[0].currentAmmo + "/" + AmmoData.instance.ammoInventory[0].maxAmmo + "\n"
                    + AmmoData.instance.ammoInventory[1].currentAmmo + "/" + AmmoData.instance.ammoInventory[1].maxAmmo + "\n"
                    + AmmoData.instance.ammoInventory[2].currentAmmo + "/" + AmmoData.instance.ammoInventory[2].maxAmmo + "\n"
                    + AmmoData.instance.ammoInventory[3].currentAmmo + "/" + AmmoData.instance.ammoInventory[3].maxAmmo;
    }
}
