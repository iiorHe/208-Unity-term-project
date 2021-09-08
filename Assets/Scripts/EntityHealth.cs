using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool selfDestructs;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selfDestructs && health <= 0){
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int val){
        if(maxHealth > 0){
            if(health - val >= maxHealth){
            health = maxHealth;
            } else {
                health -= val;
            }
        } else {
            health -= val;
        }
    }
}
