using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private Vector3 direction;
  

    void Start(){
        // -- initially go in a random direction.
        rigidbody2D = GetComponent<Rigidbody2D>();
        direction = new Vector3(1, -1, 0);
        rigidbody2D.AddForce(direction.normalized * speed * 50);
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("COLLISION");
        // -- on player, wall, or self collision change directions
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "Walls"){
            // -- direction is either 45, 135, 225, 315 degrees.
            Vector3 new_dir;
            if(direction.x > 0 && direction.y < 0){new_dir = new Vector3(-1,-1,0);}           
            else if(direction.x < 0 && direction.y < 0){new_dir = new Vector3(-1,1,0);}      
            else if(direction.x < 0 && direction.y > 0){new_dir = new Vector3(1,1,0);}      
            else if(direction.x > 0 && direction.y > 0){new_dir = new Vector3(1,-1,0);}            
            else{new_dir = -(direction);}

            rigidbody2D.AddForce(new_dir.normalized * speed * 50);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        Debug.Log("STAY");
        // -- on player, wall, or self collision change directions
        
            // -- direction is either 45, 135, 225, 315 degrees.
            Vector3 new_dir;
            if(direction.x > 0 && direction.y < 0){new_dir = new Vector3(-1,-1,0);}           
            else if(direction.x < 0 && direction.y < 0){new_dir = new Vector3(-1,1,0);}      
            else if(direction.x < 0 && direction.y > 0){new_dir = new Vector3(1,1,0);}      
            else if(direction.x > 0 && direction.y > 0){new_dir = new Vector3(1,-1,0);}            
            else{new_dir = -(direction);}

            rigidbody2D.AddForce(new_dir.normalized * speed * 50);
    }

}
