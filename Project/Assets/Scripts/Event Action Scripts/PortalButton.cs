using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalButton : MonoBehaviour
{
    // Start is called before the first frame update

    // These are used to check to see if the button it still visable
    // I should've used a list with button objects in it but, theres always next time right?
    bool firstButton,secondButton, thirdbutton;
    void Start()
    {
       gameObject.GetComponent<Renderer>().enabled = true;
       firstButton = true;
       secondButton = true;
       thirdbutton = true;
    }



    // There is three buttons for the portal and this script is used by all three
    void OnTriggerEnter2D(Collider2D collider) {

        //check to see if the player is the object touching this "button" 
        if (collider.gameObject.name == "Player") {
            //Check which button is being pressed then call the event with an int, then make the button invisible if the button is still visable
            // 1. tell the portal the button is pressed
            // 2. turn the button invisible
            // 3. make boolean false so the button can't be pressed again
            // 4. give the player 50 XP, and display this update
            if (gameObject.name == "PortalButton1" && firstButton){
                myEventManager.now.PortalItemTouch(1);
                
                gameObject.GetComponent<Renderer>().enabled = false;
                firstButton = false;
                myEventManager.now.GainXP(50);
            }

            else if (gameObject.name == "PortalButton2" && secondButton){
                myEventManager.now.PortalItemTouch(2);
               
                gameObject.GetComponent<Renderer>().enabled = false;    
                secondButton = false;
                myEventManager.now.GainXP(50);
            }

            else if (gameObject.name == "PortalButton3" && thirdbutton)
            {
                myEventManager.now.PortalItemTouch(3);
           
                gameObject.GetComponent<Renderer>().enabled = false;
                thirdbutton = false;
                myEventManager.now.GainXP(50);
       
            }

            
        }
    }
}
