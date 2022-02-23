using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to react to events
// also, I don't know if I need to let you know or not but I used this -> https://www.youtube.com/watch?v=gx0Lt4tCDE0 to help me here but (of course) I did not use it one for one
public class myEventManager : MonoBehaviour
{



    public static myEventManager now;

    public event Action<float> whenChangePlayerShotPierceAmount;

    public event Action<float> whenChangePlayerElementalDamage;

    public event Action<float> whenChangePlayerAreaOfAffect;

    public event Action<float> whenChangePlayerShotChainAmount;
    public event Action<string> whenChangePlayerClass;
    // These following lines are used to create events
    public event Action<float> whenCoinTouch;
    public event Action<float> whenHealthTouch;
    public event Action<float> whenChangePlayerMaxHealth;
    public event Action<float> whenPortalItemTouch;
    public event Action<float> whenGainXP;
    public event Action<float> whenChangePlayerMovementSpeed;
    public event Action whenChangeLuck;

    public event Action<float> whenChangePlayerDamage;

    public event Action<float> whenChangePlayerRange;

    public event Action<float> whenChangePlayerAttackSpeed;

    public event Action<float> whenChangePlayerProjectileCount;

    public event Action<float> whenChangePlayerMultiAttackCount;

    public event Action whenClassPicked;
    private void Awake() {
        now = this;
    }

    // The following are used to call events from other scripts, the "when[blank] != null" is used to see if there is a listener for that event, if there is none then don't call the event
    public void ChangePlayerClass(string className){
        if(whenChangePlayerClass != null){
            whenChangePlayerClass(className);
        }
    }
 
    public void coinTouch(float value) {

        if (whenCoinTouch != null) {
            //Debug.Log("YES coin");
            whenCoinTouch(value);
        }
    }


    public void HealthTouch(float value) {

        if (whenHealthTouch != null) {
            //Debug.Log("YES health");
            whenHealthTouch(value) ;
        }
    }

    public void ChangePlayerMaxHealth(float value){
        if(whenChangePlayerMaxHealth != null){
            whenChangePlayerMaxHealth(value);
        }
    }

    public void ChangePlayerMovementSpeed(float value){
        if(whenChangePlayerMovementSpeed != null){
            whenChangePlayerMovementSpeed(value);
        }
    }

    public void PortalItemTouch(float value) {
    
            if(whenPortalItemTouch != null){
            whenPortalItemTouch(value);
        }
    }

    public void GainXP(float value){
        if(whenGainXP != null){
            whenGainXP(value);
        }
    }

    public void ChangeLuck(){
        if(whenChangeLuck != null){
            whenChangeLuck();
        }
    }

    public void ChangePlayerDamage(float value){
        if(whenChangePlayerDamage != null){
            whenChangePlayerDamage(value);
        }

    }

    public void ChangePlayerRange(float value){
        if(whenChangePlayerRange != null){
            whenChangePlayerRange(value);
        }
    }

    public void ChangePlayerAttackSpeed(float value){
        if(whenChangePlayerAttackSpeed != null){
            whenChangePlayerAttackSpeed(value);
        }

    }

    public void ChangePlayerProjectileCount(float value){
        if(whenChangePlayerProjectileCount != null){
            whenChangePlayerProjectileCount(value);
        }

    }

    public void ChangePlayerMultiAttackCount(float value){
        if(whenChangePlayerMultiAttackCount != null){
            whenChangePlayerMultiAttackCount(value);
        }

    }


    public void ChangePlayerShotPierceAmount(float value){
        if(whenChangePlayerShotPierceAmount != null){
            whenChangePlayerShotPierceAmount(value);
        }
    }

    public void ChangePlayerElementalDamage(float value){
        if(whenChangePlayerElementalDamage != null){
            whenChangePlayerElementalDamage(value);
        }

    }

    public void ChangePlayerAreaOfAffect(float value){
        if(whenChangePlayerAreaOfAffect != null){
            whenChangePlayerAreaOfAffect(value);
        }
    
    }

    public void ChangePlayerShotChainAmount(float value){
        if(whenChangePlayerShotChainAmount != null){
            whenChangePlayerShotChainAmount(value);
        }
    }

    public void ClassPicked(){
        if(whenClassPicked != null){
            whenClassPicked();
        }

    }

}