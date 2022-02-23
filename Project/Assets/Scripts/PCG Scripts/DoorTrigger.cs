using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    // public GameObject corridorFirst;
    public GameObject[] doors;
    
    void Start() {
        Unlock();
        // -- subscribe to RoomManager's onEnemiesDefeated event
        // -- will unlock all doors ~Nolan nov 5
        RoomManager.current.onEnemiesDefeated += Unlock;
    }

    public void Lock() {
        foreach (var door in doors) {
            door.SetActive(true);
        }
    }

    public void Unlock() {
        foreach (var door in doors) {
            door.SetActive(false);
        }
    }


    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.name == "Player"){
            Lock();
            // -- tell the RoomManager the player has entered this room
            RoomManager.current.roomEntered(this.transform.parent.position);
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
