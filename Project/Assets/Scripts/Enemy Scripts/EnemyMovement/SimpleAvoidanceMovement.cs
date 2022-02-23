using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAvoidanceMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform Player;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

  
    // Start is called before the first frame update
    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
    }
    void FixedUpdate(){simpleAvoidanceMovement(); }


    private void simpleAvoidanceMovement(){
        // -- walks away from the player
        Vector2 target = Player.position;
        Vector2 dirVect = -(target - (Vector2) rigidbody2D.transform.position);
        rigidbody2D.MovePosition((Vector2)rigidbody2D.transform.position + (dirVect.normalized * speed * Time.deltaTime)); 
}
}
