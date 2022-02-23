using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour{

    // -- serialized for prefab enemys
    private float enemyCurrentHealth;
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float enemyAttackSpeed; // -- if 5, 1 attcker per 5 seconds
    // private bool usesProjectiles;
    private bool is_dead;
    // -- at Start Time
    void Start(){
        // -- have this object subscribe to these events
        // -- -- Global dynamic difficulty adjustment events
        EnemyDDAManager.current.onEnemyMaxHealthChange += modEnemyMaxHealth;
        EnemyDDAManager.current.onEnemyDamageChange += modEnemyDamage;
        EnemyDDAManager.current.onEnemyAttackSpeedChange += modEnemyAttackSpeed;
        enemyCurrentHealth = enemyMaxHealth;
        // -- assignment of stats
        is_dead = false;
    
    }
    
    // -- modify this enemy's Current health. (add or subtract)
    public void modEnemyCurrHealth(float value){
        enemyCurrentHealth += value;
        if (enemyCurrentHealth < 0){
            enemyCurrentHealth = 0;
            is_dead = true;
        }
    }

    // -- modify this enemy's Max health. (add or subtract)
    public void modEnemyMaxHealth(float value){
        enemyMaxHealth += value;
        if (enemyMaxHealth < 0){enemyMaxHealth = 0;}
    }
    
    // -- modify this enemy's damage. (increase or decrease)
    public void modEnemyDamage(float value){
        enemyDamage += value;
        if (enemyDamage < 0){enemyDamage = 0;}
    }

    // -- modify this enemy's damage. (increase or decrease)
    public void modEnemyAttackSpeed(float value){
        enemyAttackSpeed += value;
        if (enemyAttackSpeed < 1){ enemyAttackSpeed = 1;}
    }

    // -- modify this enemy's attack style. (melee or ranged)
    // private void modEnemyAttackStyle(int value){
    //     usesProjectiles = !(usesProjectiles);
    // }

    
    // -- getters
    public float getEnemyMaxHealth(){return enemyMaxHealth;}
    public float getEnemyCurHealth(){return enemyCurrentHealth;}
    public float getEnemyAttackSpeed(){return enemyAttackSpeed;}
    public float getEnemyDamage(){return enemyDamage;}
    //public bool getUsesProjectilesFlag(){return usesProjectiles;}
    public bool getIsDead(){return is_dead;}
}
