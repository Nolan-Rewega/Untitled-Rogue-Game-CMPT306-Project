using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{


    private string playerClass; // The platers class
    private float playerMoney; // This will be used to keep track of all the players money[one money = one coin]

    private float playerCurrentHealth; // This will be used to keep track of the players health
    private float playerMaxHealth;

    private float playerLevel, playerXP; // These will be used with the level system

    private float playerDamage;   // Used to store the damage the player can do, defaults is 10

    private float playerRange;    // For how far the player can attack, default is 10

    private float playerAttackSpeed;  // How fast the player attacks, defaults 5

    private bool playerLuck;    // Is the player lucky? 

    private float playerProjectileCount;  // used to store the amount of the players projectiles 

    private float playerMultiAttackCount; // Used to store the amount of the multi attack the player has
    
    private float playerShotPierceAmount;     // Stores the players shot pierece amount

    private float playerElementalDamage;  // the amount of Elemental Damage the player can deal

    private float playerAreaOfAffect;  // the area that the player attacks in

    private float playerShotChainAmount; // The amount that the player can chain attacks
    private float playerMovementSpeed;
    private bool is_dead;
    // // -- Im sorry for this      Nolan nov 08.
    [SerializeField] private Animator anim;

    
    // Start is called before the first frame update
    void Awake()
    {
        // Set the defaults
        playerMoney = 0;
        playerCurrentHealth = 100;
        playerMaxHealth = 100;
        playerLevel = 0;
        playerXP = 0;
        playerLuck = false;
        playerClass = "";
        // NOTE: the defaults I have given the following values are random values, please change
        playerDamage = 25;
        playerRange = 10;
        playerAttackSpeed = 1;
        playerProjectileCount = 1;
        playerMultiAttackCount = 1;
        playerShotPierceAmount = 1;
        playerElementalDamage = 1;
        playerAreaOfAffect = 1;
        playerShotChainAmount = 1;
        playerMovementSpeed = 1.5f;
        is_dead = false;
        // Add the functions to the correct events 
        myEventManager.now.whenChangePlayerClass += alterPlayerClass;
        myEventManager.now.whenChangePlayerMovementSpeed += alterPlayerMovementSpeed;
        myEventManager.now.whenCoinTouch += addMoney;
        myEventManager.now.whenHealthTouch += alterHealth;
        myEventManager.now.whenChangePlayerMaxHealth += alterMaxHealth;
        myEventManager.now.whenGainXP += levelSystem;
        myEventManager.now.whenChangeLuck += alterPlayerLuck;
        myEventManager.now.whenChangePlayerDamage += alterPlayerDamage;
        myEventManager.now.whenChangePlayerRange += alterPlayerRange;
        myEventManager.now.whenChangePlayerAttackSpeed += alterPlayerAttackSpeed;
        myEventManager.now.whenChangePlayerProjectileCount += alterPlayerProjectileCount;
        myEventManager.now.whenChangePlayerMultiAttackCount += alterPlayerMultiAttackCount;
        myEventManager.now.whenChangePlayerShotPierceAmount += alterPlayerShotChainAmount;
        myEventManager.now.whenChangePlayerElementalDamage += alterPlayerElementalDamage;
        myEventManager.now.whenChangePlayerAreaOfAffect += alterPlayerAreaOfAffect;
        myEventManager.now.whenChangePlayerShotChainAmount += alterPlayerShotPierceAmount;

    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public bool getIsDead(){return is_dead;}

    // This is used to change the players class,
    private void alterPlayerClass(string className){
            // -- Swaps out 
            playerClass = className; // Change the players class
            gameObject.GetComponent<Animator>().enabled = true;
            GameObject choosen;
            if(playerClass == "Range"){
                anim.runtimeAnimatorController = Resources.Load("Animation/Player") as RuntimeAnimatorController;
                GetComponent<Player_Attack>().bulletPrefab = Resources.Load("Prefabs/Arrow") as GameObject;
            }
            else if(className == "Melee"){
                anim.runtimeAnimatorController = Resources.Load("Animation/Player2") as RuntimeAnimatorController;
                GetComponent<Player_Attack>().bulletPrefab = Resources.Load("Prefabs/Arrow") as GameObject;
            }
            else if(className == "Magic"){
                anim.runtimeAnimatorController = Resources.Load("Animation/Player3") as RuntimeAnimatorController;
                GetComponent<Player_Attack>().bulletPrefab = Resources.Load("Prefabs/fireball") as GameObject;
            }
            else{choosen = gameObject;}

            myEventManager.now.whenChangePlayerClass -= alterPlayerClass; // remove the listener as the player won't be changing their class
    }

    private void alterPlayerMovementSpeed(float value){
        playerMovementSpeed += value; // Change the attackSpeed
        if(playerMovementSpeed < 1){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerMovementSpeed = 1; 
        }
    }

    // This is used to change the damage the player does 
    private void alterPlayerDamage(float value){
        playerDamage += value; // Change the attackSpeed
        if(playerDamage < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerDamage = 0; 
        }
    }

    // This is used to change the range the player has
    private void alterPlayerRange(float value){
        playerRange += value; // Change the attackSpeed
        if(playerRange < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerRange = 0; 
        }
    }



    private void alterPlayerAttackSpeed(float value){
    //Check to see if the in coming value is 0, its a waste of a event call
        playerAttackSpeed += value; // Change the attackSpeed
        if(playerAttackSpeed < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerAttackSpeed = 0; 
        }
    }
    // This changes the players luck value
    private void alterPlayerLuck(){playerLuck = !playerLuck;// make the luck value the other boolean it currenty is not

    }


    // This changes the players Projectile Count
    private void alterPlayerProjectileCount(float value){
        playerProjectileCount += value; // Change the attackSpeed
        if(playerProjectileCount < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerProjectileCount = 0; 
        }
    }
    
    private void alterPlayerMultiAttackCount(float value){
        playerMultiAttackCount += value; // Change the attackSpeed
        if(playerMultiAttackCount < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerMultiAttackCount = 0; 
        }
    }

    private void alterPlayerShotPierceAmount(float value){
        playerShotPierceAmount += value; // Change the attackSpeed
        if(playerShotPierceAmount < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerShotPierceAmount = 0; 
        }
    }

    private void alterPlayerElementalDamage(float value){
        playerElementalDamage += value; // Change the attackSpeed
        if(playerElementalDamage < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerElementalDamage = 0; 
        }
    }

    private void alterPlayerAreaOfAffect(float value){
        playerAreaOfAffect += value; // Change the attackSpeed
        if(playerAreaOfAffect < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerAreaOfAffect = 0; 
        }
    }


    private void alterPlayerShotChainAmount(float value){
        playerShotChainAmount += value; // Change the attackSpeed
        if(playerShotChainAmount < 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerShotChainAmount = 0;
        }
    }

    // This is used to increase the amount of money the player has
    private void addMoney(float value) {
        playerMoney += value;
       
        // Update the onscreen text

    }
    private void alterMaxHealth(float value){
        playerMaxHealth += value; // Change the attackSpeed
        if(playerMaxHealth <= 0){ // If the PlayerAttackSpeed is less the zero then make it zero
            playerMaxHealth = 0;
            is_dead = true;
        }
        // -- always keep player current health at max health.
        else if(playerCurrentHealth > playerMaxHealth){playerCurrentHealth = playerMaxHealth;}
    }

    // This decreases the amount of money the player has
    private void alterHealth(float value) {
        playerCurrentHealth += value;
        if(playerCurrentHealth <= 0){is_dead = true;}
        else if(playerCurrentHealth > playerMaxHealth){playerCurrentHealth = playerMaxHealth;}
    }

    // This is used for the player leveling system
    // 100 xp = 1 player level
    private void levelSystem(float value){

        // If the new XP will make total xp go over 100 increase the players level
        if(playerXP + value >= 100){
            playerLevel += 1;
            playerXP = (playerXP + value) - 100;

        }
        else{
            playerXP += value;

        }

        //Debug.Log("XP " + playerXP + " LVL " + playerLevel);

    }


    
    // Get Functions for the players stats
    public float getPlayerCurrentHealth(){
        return playerCurrentHealth;
    }
    public float getPlayerMaxHealth(){
        return playerMaxHealth;
    }

    public float getPlayerDamage(){
        return playerDamage;
    }
    
    public float getPlayerRange(){
        return playerRange;
    }

    public float getPlayerAttackSpeed(){
        return playerAttackSpeed;
    }

    public bool getPlayerLuck(){
        return playerLuck;
    }

    public float getplayerProjectileCount(){
        return playerProjectileCount;
    }

    public string getPlayerClass(){
        return playerClass;
    }

    public float getPlayerMultiAttackCount(){
        return playerMultiAttackCount;
    }

    public float getPlayerElementalDamage(){
        return playerElementalDamage;
    }

    public float getPlayerAreaOfAffect(){
        return playerAreaOfAffect;
    }

    public float getPlayerShotChainAmount(){
        return playerShotChainAmount;
    }

    public float getPlayerShotPierceAmount(){
        return playerShotPierceAmount;
    }
}
