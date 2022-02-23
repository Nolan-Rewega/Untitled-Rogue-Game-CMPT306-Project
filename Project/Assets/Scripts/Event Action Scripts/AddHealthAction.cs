using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthAction : MonoBehaviour
{
    // Start is called before the first frame update
  

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            // positive values mean adding health
            myEventManager.now.HealthTouch(10);
        }
    }



}
