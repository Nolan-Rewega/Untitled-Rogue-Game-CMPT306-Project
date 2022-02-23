using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{   
    private float damage;
    private float projectileSpeed;
    private Vector3 target;

    // -- setters
    public void setDamage(float dmg){damage = dmg;}
    public void setProjectileSpeed(float spd){projectileSpeed = spd;}
    public void setTarget(Vector3 givenTarget){target = givenTarget;}

    public void shoot(){
        Vector3 dirVect = target - (Vector3)GetComponent<Rigidbody2D>().transform.position;
        GetComponent<Rigidbody2D>().AddForce(dirVect.normalized * projectileSpeed);
    }


    void OnTriggerEnter2D(Collider2D collision){
        // -- destroy on collision
        if(collision.gameObject.name == "Player"){
            // -- deal damage to the player
            myEventManager.now.HealthTouch(-damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.name == "Walls"){ Destroy(this.gameObject);}
    }
}
