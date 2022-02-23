using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -- this class informs ALL EnemyStats SCRIPTS about a change
// -- -- usefull for dynamic difficulty adjustment
public class EnemyDDAManager : MonoBehaviour
{   
    public static EnemyDDAManager current;
    public event Action<float> onEnemyMaxHealthChange;
    public event Action<float> onEnemyDamageChange;
    public event Action<float> onEnemyMoveSpeedChange;
    public event Action<float> onEnemyAttackSpeedChange;

    // -- at Awake time.
    void Awake(){current = this;}

    public void ChangeEnemyMaxHealth(float value){
        if(onEnemyMaxHealthChange != null){ onEnemyMaxHealthChange(value); }
    }

    public void ChangeEnemyDamage(float value){
        if(onEnemyDamageChange != null){ onEnemyDamageChange(value); }
    }

    public void ChangeEnemyAttackSpeed(float value){
        if(onEnemyAttackSpeedChange != null){ onEnemyAttackSpeedChange(value); }
    }

    public void ChangeEnemyMoveSpeed(float value){
        if(onEnemyMoveSpeedChange != null){ onEnemyMoveSpeedChange(value); }
    }
}
