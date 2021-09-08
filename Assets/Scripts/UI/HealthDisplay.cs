using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthDisplay : MonoBehaviour
{
    public static HealthDisplay instance;
    private TMPro.TMP_Text _healthText;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        _healthText = GetComponent<TMPro.TMP_Text>();
        UpdateHealth();
    }
    public static void UpdateHealth(){
        instance._healthText.text = $"{PlayerBase.instance.currentHealth}%";
    }
}
