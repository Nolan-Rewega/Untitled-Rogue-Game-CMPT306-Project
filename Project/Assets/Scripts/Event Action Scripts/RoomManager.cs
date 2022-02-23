using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager current;
    private Vector3 roomCenter; // -- BAD BAD BAD????

    // -- room events
    public event Action<Vector3> onRoomEnter;
    public event Action onEnemiesDefeated;
    
    
    // Start is called before the first frame update
    private void Awake() {
        current = this;
    }

    public Vector3 getRoomCenter(){return roomCenter;} // -- BAD TEMP

    public void roomEntered(Vector3 value){
        roomCenter = value; // -- BAD BAD BAD????
        if(onRoomEnter != null){ onRoomEnter(value); }
    }

    public void enemiesDefeated(){
        if(onEnemiesDefeated != null){ onEnemiesDefeated(); }
    }






}
