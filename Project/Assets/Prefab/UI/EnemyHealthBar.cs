using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    private Image fill;
    private Vector2 localScale;
    private Color32 red, yellow, green;
    private float maxHealth;
    private EnemyStats enemyStats;
    private GameObject enemy;
    
    void Start() {
        fill = GetComponent<Image>();
        enemy = this.transform.parent.gameObject.transform.parent.gameObject;
        enemyStats = enemy.GetComponent<EnemyStats>();
        maxHealth = enemyStats.getEnemyMaxHealth();
       
        red = new Color32(255,0,0,100);
        green = new Color32(0,255,0,100);
        yellow = new Color32(255,255,0,100);
        
        localScale = transform.localScale;
   
    }

    void Update(){
        displayHealth();
    }

    public void displayHealth(){
        float currentHealth = enemyStats.getEnemyCurHealth();
        if(currentHealth > 50){fill.color = green;}
        else if(currentHealth > 25){fill.color = yellow;}
        else{fill.color = red;}
        
        localScale.x = (currentHealth / maxHealth);
        transform.localScale = localScale;
    }

    
}
