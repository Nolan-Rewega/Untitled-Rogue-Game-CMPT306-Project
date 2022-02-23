using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classManager : MonoBehaviour
{

    // This will be used to check if the player needed to pick a class
    bool needToMakeClasses;

     
    public GameObject MagicClass,MeleeClass,RangeClass;
    void Start()
    {   
        // Start off that the player needs to pick a class
        needToMakeClasses = true;
    }
    

    void Awake(){
        needToMakeClasses = true;
        makeClasses();

    }
    // Update is called once per frame 
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T)){makeClasses();} // This is here to test, I did not know where to call the function
        
        
    }


    // This function puts the different class items into the starting room
    public void makeClasses(){
        //Check to see if the player need to pick a class
        if(needToMakeClasses == true){
           GameObject Melee = Instantiate(MeleeClass);
           GameObject Magic = Instantiate(MagicClass);
           GameObject Ranged = Instantiate(RangeClass);
          
        }

    }
}
