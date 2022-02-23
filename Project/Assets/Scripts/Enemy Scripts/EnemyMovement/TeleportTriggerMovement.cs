using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTriggerMovement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private float waitTime;
    private bool is_teleporting;

    private Animator animator;
    private SpriteRenderer sr;
    public Sprite test_sprite;
    public Sprite main_sprite;

    // Start is called before the first frame update
    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        waitTime = 2.0f;
    }

    // temp 
    private IEnumerator randomTeleport(){
        // -- disable attacking
        gameObject.GetComponent<FireProjectiles>().enabled = false;

        yield return new WaitForSeconds(waitTime);
        
        // -- reinable attacking
        gameObject.GetComponent<FireProjectiles>().enabled = true;
        
        // -- get random position (CHANGE)
        Vector3 room_center = RoomManager.current.getRoomCenter();
        Vector3 pos = new Vector3(0,0,0);
        pos.x = room_center.x + Random.Range(-10.0f, 10.0f);
        pos.y = room_center.y + Random.Range(-10.0f, 10.0f);
        
        this.transform.position = pos;
        // -- change to teleport main sprite
        sr.sprite = main_sprite;
        is_teleporting = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        // -- when player entesr its teleport radius
        if(collider.gameObject.name == "Player" && !(is_teleporting)){
            // -- telegraph teleport
            is_teleporting = true;
            sr.sprite = test_sprite;
            StartCoroutine(randomTeleport());
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        // -- when they take damage
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.layer);
        if((collision.gameObject.layer == 6 && !(is_teleporting))){
            is_teleporting = true;
            sr.sprite = test_sprite;
            StartCoroutine(randomTeleport());
        }
    }


}
