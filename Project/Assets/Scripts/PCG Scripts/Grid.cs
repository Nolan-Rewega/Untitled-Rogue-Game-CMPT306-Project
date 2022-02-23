using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grid : MonoBehaviour
{


    private CorridorFirstDungeonGenerator dungeon;
    
    //private Tile[,] grid;
    private List<Tile> list_grid;
    //private List<Tile> tilesByRoomCenter;

 
    
    // -- create grid at Awake time
    void Start(){
        list_grid = new List<Tile>();
        dungeon = GameObject.Find("CorridorFirstDungeonGenerator").GetComponent<CorridorFirstDungeonGenerator>();
        //-- might be too soon to convert
        convertToGrid();
    }

    // -- returns the list size.
    public int getGridSize(){ return list_grid.Count;}


    // -- gets the tile form list_grid that matches the give Vector2 position
    public Tile getTileFromPosition(Vector2 position) {
        if (list_grid.Count != 0){
            foreach (Tile t in list_grid){
                if(t.getWorldPosition() == position){ return t;}
            }
        }
        // -- error case (not sure how to handle)
        return new Tile(new Vector2(-1,-1), false, -1);
    }


    public List<Tile> getNeighboursOfTile(Tile tile) {
        // -- getting 8 neighbours (8*O(n^2))
        // -- start (4,7)
        // -- (3,6) (3,7) (3,8)
        // -- (4,6)       (4,8)
        // -- (5,6) (5,7) (5,8)
        float x = tile.getWorldPosition().x;
        float y = tile.getWorldPosition().y;
        List<Tile> neighbours = new List<Tile>();

        for(float r = x-1; r < x+2; r++ ){
            for(float c = y-1; c < y+2; c++){
                // -- kd tree or some other structure log(n) query
                foreach (Tile t in list_grid){
                    if(t.getWorldPosition() == new Vector2(r, c)){
                        neighbours.Add(t);
                    }
                }
            }
        }
        return neighbours;

    }

    // -- converts nathan's PCG dungeon floor tiles (walkable tiles) to a list of object tiles
    private void convertToGrid(){
        // -- call nathan's getFloorPositions method.
        HashSet<Vector2Int> floor_tiles = dungeon.GetFloorPositions();
        foreach (Vector2Int position in floor_tiles){
            Vector2 converted = new Vector2((float) position.x, (float)position.y);
            list_grid.Add(new Tile(converted, true, 1));
        }
    }

}
