using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : IHeapItem<Tile>{

    private bool is_walkable;
    private int cellsize;
    Vector2 world_position;


    // -- costs for pathfinding
    private int h_cost;
    private int g_cost;
    // -- setting partent establish a 'chain' from start Tile to target Tile
    private Tile parent;
    // -- IHeapItem attribute
    private int heap_index;


    public Tile(Vector2 position, bool walkable, int cell_size){
        world_position = position;
        is_walkable = walkable;
        cellsize = cell_size;
    }

    // -- getters
    public bool getIsWalkable(){return is_walkable;}
    public Vector2 getWorldPosition(){return world_position;}
    public int getHCost(){return h_cost;}
    public int getGCost(){return g_cost;}
    public int getCellSize(){return cellsize;}
    public Tile getParent(){return parent;}

    // -- setters
    public void setWalkable(bool value){is_walkable = value;}
    public void setWorldPosition(Vector2 new_pos){world_position = new_pos;}
    public void setHCost(int new_h_cost){h_cost = new_h_cost;}
    public void setGCost(int new_gcost){g_cost = new_gcost;}
    public void setCellSize(int new_cellsize){cellsize = new_cellsize;}
    public void setParent(Tile new_parent){parent = new_parent;}

    // -- helper / convenience functions
    public int fCost(){ return g_cost + h_cost;}
    public override string ToString(){
        return "world_pos: " + world_position.ToString() + ", H_cost: " + h_cost.ToString()
        + ", Gcost: " + g_cost.ToString() + ", Walkable: "+ is_walkable.ToString();
    }

    // -- interface method for the heap A* optimization
    public int HeapIndex{
        get {return heap_index;}
        set {heap_index = value;}
    }

    // -- interface method for the heap A* optimization
    public int CompareTo(Tile tile){
        int compare_flag = fCost().CompareTo(tile.fCost());
        if (compare_flag == 0){
            compare_flag = h_cost.CompareTo(tile.getHCost());
        }
        return -compare_flag;

    }
}
