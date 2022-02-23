using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

/**  Inspired by Sebastian Lague tutorials for A* search AI's
 *  Credit Sebastian Lague for A* code tutorial(s): 
 *  https://www.youtube.com/watch?v=mZfyt03LDH4 
 *  https://www.youtube.com/watch?v=dn1XRIaROM4
**/
public class AStarPathfinding : MonoBehaviour{

    // -- have references to the A* manager and grid objects
    private Grid grid;
    private AStarManager manager;

    void Start() {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        manager = GetComponent<AStarManager>();
    }

    // -- begins a A* search request, which is requested from the manager
    public void startRequest(Vector3 start, Vector3 target){
        StartCoroutine(findPath(start, target));
    }


    // -- A* search implementation
    // -- -- IEnumerator to allow stoping and starting of disjoint requests
    public IEnumerator findPath(Vector3 start, Vector3 target){
        Tile start_tile = grid.getTileFromPosition(start);
        Tile target_tile = grid.getTileFromPosition(target);
        
        // -- defining the data structures that store Tile data 
        // -- Open set is the set of Tiles that have not been proccessed
        // -- Closed set is the set of Tiles that have been proccessed
        Heap<Tile> open_set = new Heap<Tile>(grid.getGridSize());
        HashSet<Tile> closed_set = new HashSet<Tile>();

        bool path_found = false;
        Vector3[] waypoints = new Vector3[0]; 

        open_set.push(start_tile);

        // -- loop until open set is empty
        while(open_set.Count() > 0){
            // -- pop() from a heap in O(log(n)) time
            Tile current = open_set.pop();
            closed_set.Add(current);
            
            // -- base case (target is found)
            if(current == target_tile){
                path_found = true;
                break;
            }
            
            // -- check each update each neighbour's cost and add it to the open set
            foreach(Tile neighbour in grid.getNeighboursOfTile(current)){
                if((!neighbour.getIsWalkable()) || closed_set.Contains(neighbour)){continue;}
                
                int newMovementCostToNeighbour = current.getGCost() + getDistance(current, neighbour);
                // -- if neighbour not in open, or if new path to neighbour from current is less than its own Gcost
                if(newMovementCostToNeighbour < neighbour.getGCost() || !open_set.has(neighbour)){
                    // -- update tiles' costs and parent
                    neighbour.setGCost(newMovementCostToNeighbour);
                    neighbour.setHCost(getDistance(neighbour, target_tile));
                    neighbour.setParent(current);
                    
                    // -- add neighbour to open set
                    if(!open_set.has(neighbour)){open_set.push(neighbour);}
                    else{open_set.updateItem(neighbour);}
                }
            }
        }
        
        // -- wait 1-frame before returning.
        yield return null;
        if (path_found){ 
            waypoints = backtrack(start_tile, target_tile);
            path_found = waypoints.Length > 0;
        
        }
        manager.finishedProccessing(waypoints, path_found);
        
    }

    // -- once target is found backtrack from the target to the start, using the parent
    // -- hierarchy
    private Vector3[] backtrack(Tile start, Tile target){
        List<Tile> path = new List<Tile>();
        Tile current = target;

        while(current != start){
            path.Add(current);
            current = current.getParent();
        }
        // -- grid.setpath is used to draw the current A* path
        Vector3[] waypoints = findPivotTiles(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    // -- On the path, get the tiles that change the agents direction
    private Vector3[] findPivotTiles(List<Tile> path){
        List<Vector3> waypoints = new List<Vector3>(); 
        Vector2 old_direction = Vector2.zero;

        // -- checking if the [x, y] direction has changed.
        for(int i = 1; i < path.Count; i++){
            Vector2 new_direction = new Vector2(
                path[i-1].getWorldPosition().x - path[i].getWorldPosition().x,
                path[i-1].getWorldPosition().y - path[i].getWorldPosition().y 
            );
            // -- if direction has changed, add it to the waypoints
            if(new_direction != old_direction){
                waypoints.Add(new Vector3(path[i].getWorldPosition().x, path[i].getWorldPosition().y ,-1));
            }
            old_direction = new_direction;
        }
        return waypoints.ToArray();
    }

    // -- calculates the minimum cost path of movement between two tiles.
    // -- diagonal movement is costs 14, horizontal and vertical costs 10
    private int getDistance(Tile A, Tile B){
        int x_distance = Mathf.Abs( (int) A.getWorldPosition().x - (int) B.getWorldPosition().x);
        int y_distance = Mathf.Abs((int) A.getWorldPosition().y - (int) B.getWorldPosition().y);

        return 14*Mathf.Min(x_distance, y_distance) + 10*Mathf.Abs(x_distance-y_distance);
    }


}
