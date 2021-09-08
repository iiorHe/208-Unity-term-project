using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int currentHealth;
    private int maxHealth = 100;
    public static PlayerBase instance;
    public GameObject weapons;
    public Canvas userInterace;
    public Camera playerCamera;
    public List<AudioClip> painGrunts;
    private AudioSource _source;
    
    private void Awake() {
        instance = this;
    }
    private void Start() {
        _source = GetComponent<AudioSource>();
    }
    public void TakeDamage(int val){
        currentHealth -= val;
        _source.PlayOneShot(painGrunts[Random.Range(0,painGrunts.Count - 1)]);
        HealthDisplay.UpdateHealth();
    }
    public void TakeHealing(int val){
        if(currentHealth + val >= maxHealth){
        currentHealth = maxHealth;
        } else {
            currentHealth += val;
            if(val < 0){
                _source.PlayOneShot(painGrunts[Random.Range(0,painGrunts.Count - 1)]);
            }
        }
        HealthDisplay.UpdateHealth();
    } 
}
