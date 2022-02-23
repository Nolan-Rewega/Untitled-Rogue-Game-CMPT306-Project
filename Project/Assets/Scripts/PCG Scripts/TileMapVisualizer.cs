using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System;

public class TileMapVisualizer : MonoBehaviour {
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap;

    [SerializeField]
    private TileBase doors, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornerDownLeft,
    wallInnerCornerDownRight, wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpLeft,
    wallDiagonalCornerUpRight;

    [SerializeField]
    private List<TileBase> floorTiles; 

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        PaintTiles(floorPositions, floorTileMap, floorTiles);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, List<TileBase> tiles) {
        foreach (var position in positions) {
            PaintSingleTile(tileMap, tiles[Random.Range(0, tiles.Count)], position);
        }
    }

    internal void PaintSingleDoor(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.doors.Contains(typeAsInt)) {tile = doors;
        }
        if (tile != null) {
            PaintSingleTile(floorTileMap, tile, position);
        }

    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt)) { tile = wallTop;
        } 
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt)) {tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt)) {tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt)) {tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt)) {tile = wallFull;
        }

        if (tile != null) {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
         TileBase tile = null;

         if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) { tile = wallInnerCornerDownLeft;
        } 
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)) {tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) {tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) {tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) {tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) {tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt)) {tile = wallFull;
        }
        else if (WallTypesHelper.wallBottomEightDirections.Contains(typeAsInt)) {tile = wallBottom;
        }

         if (tile != null) {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position) {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear() {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }
}
