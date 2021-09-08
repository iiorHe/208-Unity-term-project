using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool switchNewWeapon;
    public int desiredWeaponIndex = 0;
    public int currentWeaponIndex;
    public bool[] weaponInventory = new bool[System.Enum.GetValues(typeof(WeaponType)).Length];
    public List<int> availableInventory;
    public  int _scrollWeapon = 0;
    
    // Start is called before the first frame update~
    void Start()
    {
        UpdateInventory();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        InventoryInput();
        if(weaponInventory[desiredWeaponIndex]){
            SelectWeapon();
        }
    }
    void SelectWeapon(){
        for(int i = 0; i < transform.childCount; i++){
            if(i != desiredWeaponIndex){
                HideWeapon(i);
            } else {
                if(weaponInventory[i]){
                    BringUpWeapon(i);
                } else {
                    HideWeapon(i);
                }
                
            }
        }
    }
    void InventoryInput(){
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            if(_scrollWeapon >= availableInventory.Count - 1) _scrollWeapon = 0;
            else _scrollWeapon++;
            desiredWeaponIndex = availableInventory[_scrollWeapon];
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(_scrollWeapon <= 0) _scrollWeapon = availableInventory.Count -1;
            else _scrollWeapon--;
            desiredWeaponIndex = availableInventory[_scrollWeapon];
        }
        switch(Input.inputString){
            case "1":
                desiredWeaponIndex = 0;
                Debug.Log(Input.inputString);
            break;
            case "2":
                desiredWeaponIndex = 1;
                Debug.Log(Input.inputString);
            break;
            case "3":
                desiredWeaponIndex = 2;
                Debug.Log(Input.inputString);
            break;
            case "4":
                desiredWeaponIndex = 3;
                Debug.Log(Input.inputString);
            break;
            case "5":
                desiredWeaponIndex = 4;
                Debug.Log(Input.inputString);
            break;
            case "G":
                Debug.Log("Drop weapon");
            break;
            default:
            break;
        }
        
    }
    private void HideWeapon(int index){
        foreach(Transform frame in transform.GetChild(index)) frame.gameObject.SetActive(false);
        transform.GetChild(index).gameObject.SetActive(false);
    }
    private void BringUpWeapon(int index){
        currentWeaponIndex = index;
        transform.GetChild(index).gameObject.SetActive(true);
        foreach(Transform frame in transform.GetChild(index)) frame.gameObject.SetActive(false);
    }
    public void UpdateInventory(){
        int i = 0;
        availableInventory.Clear();
        foreach(bool owned in weaponInventory){
            if(owned){
                availableInventory.Add(i);
            }
            i++;
        }
    }
}
    public enum WeaponType{
        Axe,
        Pistol,
        Shotgun,
        SSG,
        MP40
    }
