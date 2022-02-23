using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // -- see jorden's player listen to see what each stat is / does
    // -- set unwanted stats to 0, as there are ADDED to player stats.
    // -- use negative values will subtract from player stats
    [SerializeField] private float increaseMaxHealth;
    [SerializeField] private float increaseCurrentHealth;
    [SerializeField] private float increaseDamage;
    [SerializeField] private float increaseMovementSpeed;
    [SerializeField] private float increaseRange;
    [SerializeField] private float increaseAttackSpeed;
    [SerializeField] private bool setLuckFlag;
    [SerializeField] private float increaseNumberOfProjectile;
    [SerializeField] private float increaseMultiAttackCount;
    [SerializeField] private float increaseShotPierceAmount;
    [SerializeField] private float increaseElementalDamage;
    [SerializeField] private float increaseAreaOfEffect;
    [SerializeField] private float increaseShotChainAmount;


    // -- on player trigger
    void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.name == "Player"){
            // -- play item pick up sound
            // -- -- TODO
            // -- play item pick up animation
            // -- -- TODO
            Debug.Log("Touched");
            // -- add prefab stats to player
            if(setLuckFlag){myEventManager.now.ChangeLuck();}
            myEventManager.now.HealthTouch(increaseCurrentHealth);
            myEventManager.now.ChangePlayerMaxHealth(increaseMaxHealth);
            myEventManager.now.ChangePlayerDamage(increaseDamage);
            myEventManager.now.ChangePlayerRange(increaseRange);
            myEventManager.now.ChangePlayerAttackSpeed(increaseAttackSpeed);
            myEventManager.now.ChangePlayerProjectileCount(increaseNumberOfProjectile);
            myEventManager.now.ChangePlayerMultiAttackCount(increaseMultiAttackCount);
            myEventManager.now.ChangePlayerShotPierceAmount(increaseShotPierceAmount);
            myEventManager.now.ChangePlayerElementalDamage(increaseElementalDamage);
            myEventManager.now.ChangePlayerAreaOfAffect(increaseAreaOfEffect);
            myEventManager.now.ChangePlayerShotChainAmount(increaseShotChainAmount);

            // -- destory all humans
            Destroy(gameObject);
        }

    }

}
