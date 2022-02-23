using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinusHealthAction : MonoBehaviour
{
    
   
    void OnTriggerEnter2D(Collider2D collider) {

        if (collider.gameObject.name == "Player") {
            // Negative values remove health
            myEventManager.now.HealthTouch(-10);
        }
    }

}
