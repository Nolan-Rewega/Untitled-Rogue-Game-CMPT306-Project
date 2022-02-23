using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



// This script will undertake all the actions that the coin needs
public class CoinAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.makeRandomCoin();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Y)) {this.makeRandomCoin();}
    }

    // When something touchs the coin 
    void OnTriggerEnter2D(Collider2D collider){

        // Checks if the player is the player, if yes then give the player 1 money, 10 xp then put the coin in a random place
        if (collider.gameObject.name == "Player"){
            //Debug.Log("Here");
            myEventManager.now.coinTouch(1);
            myEventManager.now.GainXP(10);
            
           //this.makeRandomCoin();

        }

    }


// I will leave this here but I don't think it'll help anything, if you do use it you'll need to change the Random stuff
/*
 public void makeRandomCoin(){

        this.transform.position = new Vector2(Random.Range(-12.5f, 12.5f),Random.Range(-5.5f, 5.5f));
    }
 */
} 
