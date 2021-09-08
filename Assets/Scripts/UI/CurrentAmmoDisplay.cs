using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentAmmoDisplay : MonoBehaviour
{
    public static CurrentAmmoDisplay instance;
    private WeaponManager _weapons;
    private TMPro.TMP_Text _ammoText;
    private void Awake() {
        instance = this;
        _ammoText = GetComponent<TMPro.TMP_Text>();
    }
    private void Start(){
        _weapons = PlayerBase.instance.weapons.GetComponent<WeaponManager>();
    }
    public static void UpdateWeaponText(){
        instance._ammoText.text = "lol";
    }

}
