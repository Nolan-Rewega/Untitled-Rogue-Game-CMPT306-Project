using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusMovementTrigger : MonoBehaviour
{
    private EnemyAStar search;
    private Vector3 room_center;

    // Start is called before the first frame update
    void Start(){
        search = GetComponent<EnemyAStar>();
        room_center = RoomManager.current.getRoomCenter();
    }

    // -- check each frame to see if player is in trigger
    void OnTriggerStay2D(Collider2D collider){
        // -- when player enters its radius or player projectile
        if(collider.gameObject.name == "Player"){
            // -- generate a random position, pos need to be in the physical grid
            Vector3 pos = new Vector3(0,0,0);
            pos.x = room_center.x + Random.Range(-7.0f, 7.0f);
            pos.y = room_center.y + Random.Range(-7.0f, 7.0f);
            // -- request path to object
            search.request(pos);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        // -- when they take damage from player projectile
        if(collision.gameObject.layer == 6){
            Vector3 pos = new Vector3(0,0,0);
            pos.x = room_center.x + Random.Range(-7.0f, 7.0f);
            pos.y = room_center.y + Random.Range(-7.0f, 7.0f);
            // -- request path to object
            search.request(pos);
        }
    }



}
