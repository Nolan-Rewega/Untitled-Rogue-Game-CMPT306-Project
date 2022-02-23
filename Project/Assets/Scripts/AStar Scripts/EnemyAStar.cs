using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Inspired by Sebastian Lague A* pathfinding tutorial:
*  https://www.youtube.com/watch?v=dn1XRIaROM4
*/

// -- THIS SCRIPT GIVES THE ENEMEY MOVEMENT
public class EnemyAStar : MonoBehaviour {
    
    [SerializeField] private float move_speed;

    private Vector3[] current_path;
    private int pos_in_path_idx;
    private bool following_path = false;

    // -- on start time send a request to the manager for a A* search path
    void Start(){
        // -- manually request on from external script on start
        // -- AStarManager.requestPath(transform.position, target, OnPathFound);
    }

    // -- request a path every frame unless already following one.
    // -- changed to on call from on update.
    public void request(Vector3 givenTarget) {
        if(!following_path){
            following_path = true;
            AStarManager.requestPath(transform.position, givenTarget, OnPathFound);
        }
    }


    // -- If a valid path is found, follow it
    public void OnPathFound(Vector3[] new_path, bool path_found){
        following_path = false;
        if(path_found){
            current_path = new_path;
            StopCoroutine("followPath");
            StartCoroutine("followPath");
        }
    }

    // -- moves the current enemy toward a waypoint
    private IEnumerator followPath(){
        Vector3 current_waypoint = current_path[0];
        while(true){
            // -- Once the enemy reaches the current waypoint, set a new waypoint 
            if(transform.position == current_waypoint){
                pos_in_path_idx++;
                if (pos_in_path_idx >= current_path.Length){ yield break;}
                current_waypoint = current_path[pos_in_path_idx];
            } 

            transform.position = Vector3.MoveTowards(transform.position, current_waypoint, move_speed * Time.deltaTime);
            // -- wait for 1-frame
            yield return null;    
        }
    }
}
