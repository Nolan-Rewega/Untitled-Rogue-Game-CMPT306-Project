using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/* 
* Heavily insprired by sebastian lague's tutorial 
* on A* search: https://www.youtube.com/watch?v=dn1XRIaROM4 
*/
public class AStarManager : MonoBehaviour{
    // --  we need a reference to the A* search algorithm to calculate requests
    private AStarPathfinding a_star;

    // --  queue used to proccess requests in recieved order
    private Queue<PathRequest> PRQueue = new Queue<PathRequest>();
    private PathRequest current_request;
    private static AStarManager instance;
    private bool is_processing;

    // -- create a instance and get the A_star on Awake time
    void Awake() {
        instance = this;
        a_star = GetComponent<AStarPathfinding>();
    }
    public bool getIsProccessing(){return is_processing;}
    
    // -- Enemys make A* requests via this function
    public static void requestPath(Vector3 start, Vector3 target, Action<Vector3[], bool> callback){
        PathRequest new_request = new PathRequest(start, target, callback);
        instance.PRQueue.Enqueue(new_request);
        instance.tryProccessingNext();
    }

    // --  Manager notifies the caller that the path has been computed
    public void finishedProccessing(Vector3[] path, bool status){
        current_request.callback_status(path, status);
        is_processing = false;
        tryProccessingNext(); 
    }

    // -- Manager trys to proccess the next request if there is no other requests being proccess
    private void tryProccessingNext(){
        if(!is_processing && PRQueue.Count > 0){
            current_request = PRQueue.Dequeue();
            is_processing = true;
            a_star.startRequest(current_request.start, current_request.end);
        }
    }

    // -- Data structure for defining requests
    struct PathRequest{
        public Vector3 start;
        public Vector3 end;
        public Action<Vector3[], bool> callback_status;

        public PathRequest(Vector3 new_start, Vector3 new_end, Action<Vector3[], bool> new_callback){
            start = new_start;
            end = new_end;
            callback_status = new_callback;
        }
    }





}
