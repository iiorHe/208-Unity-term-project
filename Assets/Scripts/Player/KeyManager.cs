using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public bool[] keyInventory = new bool[System.Enum.GetValues(typeof(KeyColor)).Length];
    
}
public enum KeyColor{
    Red,
    Blue,
    Yellow
}
