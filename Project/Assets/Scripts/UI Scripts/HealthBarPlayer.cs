using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarPlayer : MonoBehaviour
{
    private Image fill;
    private Vector2 localScale;
    private Color32 red, yellow, green;
    private float maxHealth;
    private PlayerListener playerStats;

    [SerializeField] private GameObject player;
    
    void Start() {
        fill = GetComponent<Image>();
        playerStats = player.GetComponent<PlayerListener>();
        
        maxHealth = playerStats.getPlayerMaxHealth();
        
        red = new Color32(255,0,0,100);
        green = new Color32(0,255,0,100);
        yellow = new Color32(255,255,0,100);
        
        localScale = transform.localScale;
   
    }

    void Update(){
        displayHealth();
    }

    public void displayHealth(){
        float currentHealth = (float) playerStats.getPlayerCurrentHealth();
        if(currentHealth > 50){fill.color = green;}
        else if(currentHealth > 25){fill.color = yellow;}
        else{fill.color = red;}
        
        localScale.x = currentHealth / maxHealth;
        transform.localScale = localScale;
    }

    
}

