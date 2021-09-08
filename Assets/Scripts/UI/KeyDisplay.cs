using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDisplay : MonoBehaviour
{
    static Transform _keyDisplay;
    private void Awake() {
        _keyDisplay = transform;    
    }
    public static void UpdateKeyUI(int index){
        _keyDisplay.GetChild(index).gameObject.SetActive(true);
    }
}
