using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject corridorFirstDungeonGenerator;


    public void Spawn() {
        corridorFirstDungeonGenerator = GameObject.Find("CorridorFirstDungeonGenerator");
        Vector2Int startPosition = corridorFirstDungeonGenerator.GetComponent<CorridorFirstDungeonGenerator>().bossRoomPosition;
        transform.position = (Vector3Int) startPosition;
    }
}
