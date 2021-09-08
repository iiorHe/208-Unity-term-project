using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHurt : MonoBehaviour
{
    public int damage;
    public float damageRate;
    public bool damagePlayer,damageEnv,damageEnemies;
    private float _timer;
    private void OnCollisionStay(Collision other) {
        DamageUpdate(other.transform);
    }
    private void OnCollisionExit(Collision other) {
        _timer = 0;
    }
    private void OnTriggerStay(Collider other) {
        DamageUpdate(other.transform);
    }
    private void OnTriggerExit(Collider other) {
        _timer = 0;
    }
    private void DamageUpdate(Transform target){
        if(_timer <= 0){
            TargetAction(target);
            _timer = damageRate;
        }
        _timer -= Time.deltaTime;
    }
    private void TargetAction(Transform target){
        switch(target.tag){
            case "Player":
                if(target.TryGetComponent<PlayerBase>(out PlayerBase player)){
                    player.TakeDamage(damage);
                }
            break;
            case "Enemy":
                if(target.TryGetComponent<EntityHealth>(out EntityHealth enemy)){
                    enemy.TakeDamage(damage);
                }
            break;
            case "Destructable":
                if(target.TryGetComponent<EntityHealth>(out EntityHealth obj)){
                    obj.TakeDamage(damage);
                }
            break;
            default:
            break;
        }
    }
}
