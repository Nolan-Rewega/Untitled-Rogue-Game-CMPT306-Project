using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used for the player to pick their class
public class playerClassPicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myEventManager.now.whenClassPicked += removeThis;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// WHen the player touchs one of the class items see which one then tell the player listener it
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
         
            if(this.gameObject.name == "ClassMelee(Clone)"){
                
                myEventManager.now.ChangePlayerClass("Melee");
            }
            else if(this.name == "ClassRanged(Clone)"){
         
                myEventManager.now.ChangePlayerClass("Ranged");
            }
            else if(this.name == "ClassMagic(Clone)"){
                
                myEventManager.now.ChangePlayerClass("Magic");
            }
            else{Debug.Log("THe class is not found");}

           myEventManager.now.ClassPicked(); // Let all other classes know one has been picked
           
        }
    }

    // This function destorys the current game Object
    private void removeThis(){
        Destroy(this.gameObject);
    }

}
