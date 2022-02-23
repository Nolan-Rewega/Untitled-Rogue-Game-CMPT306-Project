using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAction : MonoBehaviour
{


    // Will need to rework this script when using it for the game

    private bool buttonOne,buttonTwo,buttonThree,visible;

    // Start is called before the first frame update
    void Start()
    {
        buttonOne = false;
        buttonTwo = false;
        buttonThree = false;
        visible = false;
 
        //Make the sprite invisible 
        gameObject.GetComponent<Renderer>().enabled =false;
        // Add the checkButtons function to the "listner list" for whenPortalItemTouch
        myEventManager.now.whenPortalItemTouch += checkButtons;
    }

    // Update is called once per frame
    void Update()
    {
        if (visible) {
            gameObject.GetComponent<Renderer>().enabled = true;
            //Remove the function as it is no longer needed
            myEventManager.now.whenPortalItemTouch -= checkButtons;
            
        }
    }

    //There are 3 buttons that need to be touched for the portal to appear 
    private void checkButtons(float buttonNumber) {
        
        //Check to see what is the incoming button
        if (buttonNumber == 1) { buttonOne = true;}
        else if (buttonNumber == 2) { buttonTwo = true; }
        else if (buttonNumber == 3) { buttonThree = true; }
        else { Debug.Log("OH NO that number is not right"); }

        //Check to see if all the buttons are now touched(true)
        if (buttonOne && buttonTwo && buttonThree) {
           // Debug.Log("Hereer");
            visible = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        //Check if the the portal is "open" and if the player is touching it
        if (visible && collider.gameObject.name == "Player") {
            
            
                //Debug.Log("GET READY TO MOVE!!!");

            
            
        
        
        }
        
    }





}
