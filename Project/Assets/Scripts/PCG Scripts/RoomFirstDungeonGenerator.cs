// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using UnityEngine.UI;
// using Random = UnityEngine.Random;

// public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator {

//     private int count = 0;

//     [SerializeField]
//     private int minRoomWidth = 10, minRoomHeight = 10;
//     [SerializeField]
//     private int maxRoomWidth = 1000, maxRoomHeight = 1000;
    
//     private int dungeonWidth = 20, dungeonHeight = 20;

//     [SerializeField]
//     private int bossRoomWidth = 50, bossRoomHeight = 50;

//     [SerializeField]
//     private int number_of_rooms = 1;

//     [SerializeField]
//     [Range(1, 10)]
//     private int offset = 1;

//     public Vector3Int start_room_position;

//     public Vector3Int boss_start_poition;

//     private GameObject player;

//     private GameObject Boss;

//     protected override void RunProceduralGeneration() {
//         InputField input = GameObject.Find("Input_RoomCount").GetComponent<InputField>();
//         String inputRoomCount = input.text;
//         if (inputRoomCount != "") {
//             number_of_rooms = int.Parse(inputRoomCount);
//         }
//         CreateRooms();

//     }

//     private void CreateRooms() {

//         if (number_of_rooms > 0) {
//             // try to generate rooms, if dungeon is too small increase dungeon
//             try {
//                 var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int
//                 (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight, bossRoomWidth, bossRoomHeight, number_of_rooms);

//                 // Debug.Log(roomsList[0].size.x);

//                 HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
//                 floor = CreateSimpleRooms(roomsList);

//                 var largestRoom = roomsList[0];
//                 // var bossRoomIndex = 0;

//                 List<Vector2Int> roomCentres = new List<Vector2Int>();
//                 foreach (var room in roomsList) {
//                     if (room.size.x > largestRoom.size.x) {
//                         // bossRoomIndex++;
//                         largestRoom = room;
//                     }
//                     roomCentres.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
//                 }

//                 var largestRoomCentre = (Vector2Int)Vector3Int.RoundToInt(largestRoom.center);
//                 // roomCentres.Remove(largestRoomCentre);
//                 // roomCentres.Add(largestRoomCentre);

//                 // Debug.Log("BOSS Index " + bossRoomIndex);
//                 // Debug.Log("Largest room " + largestRoom);


//                 HashSet<Vector2Int> corridors = ConnectRooms(roomCentres, largestRoomCentre);
//                 floor.UnionWith(corridors);

//                 tilemapVisualizer.PaintFloorTiles(floor);
//                 WallGenerator.CreateWalls(floor, tilemapVisualizer);
//             } catch (Exception) {
//                 count++;
//                 if (count < 20) {
//                     dungeonWidth = dungeonWidth + 10;
//                     dungeonHeight = dungeonHeight + 10;
//                     CreateRooms();
//                 }
//                 Debug.Log("Could not generate dungeon");
//             } finally {
//                 // reset dungeon to 20 by 20
//                 dungeonWidth = 20;
//                 dungeonHeight = 20;
//                 count = 0;
//             }

//             player = GameObject.Find("Player");
//             player.GetComponent<PlayerMovement>().EnterDungeon();
//             Boss = GameObject.Find("Boss");
//             Boss.GetComponent<BossSpawn>().Spawn();
//         }
//     }

//     private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCentres, Vector2Int largestRoomCentre) {
//         HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

//         // var currentRoomCentre = roomCentres[0];
//         // if (currentRoomCentre == largestRoomCentre) {
//         //     currentRoomCentre = roomCentres[1];
//         // }
//         var currentRoomCentre = roomCentres[Random.Range(0, roomCentres.Count)];


    
//         // boss_start_poition = (Vector3Int) currentRoomCentre;
//         start_room_position = (Vector3Int) currentRoomCentre;

//         // Debug.Log("Current Room " + currentRoomCentre);
//         // Debug.Log("Start " + start_room_position);

//         while (roomCentres.Count > 0) {
//             Vector2Int closest = FindClosestPointTo(currentRoomCentre, roomCentres);
//             // if (closest == largestRoomCentre) {
//             //     boss_start_poition = (Vector3Int) closest;
//             // }
//             roomCentres.Remove(closest);
//             HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCentre, closest);
//             currentRoomCentre = closest;
//             corridors.UnionWith(newCorridor);
//             if (roomCentres.Count == 0) {
//                 // start_room_position = (Vector3Int) currentRoomCentre;
//                 boss_start_poition = (Vector3Int) currentRoomCentre;
//             }
//         }
//         return corridors;
//     }

//     private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCentre, Vector2Int destination) {
//         HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
//         var position = currentRoomCentre;
//         corridor.Add(position);
//         while (position.y != destination.y) {
//             if (destination.y > position.y) {
//                 position += Vector2Int.up;
//             } else if (destination.y < position.y) {
//                 position += Vector2Int.down;
//             }
//             corridor.Add(position);
//         }
//         while (position.x != destination.x) {
//             if (destination.x > position.x) {
//                 position += Vector2Int.right;
//             } else if (destination.x < position.x) {
//                 position += Vector2Int.left;
//             }
//             corridor.Add(position);
//         }
//         return corridor;
//     }

//     private Vector2Int FindClosestPointTo(Vector2Int currentRoomCentre, List<Vector2Int> roomCentres) {
//         Vector2Int closest = Vector2Int.zero;
//         float distance = float.MaxValue;
//         foreach (var position in roomCentres) {
//             float currentDistance = Vector2Int.Distance(position, currentRoomCentre);
//             if (currentDistance < distance) {
//                 distance = currentDistance;
//                 closest = position;
//             }
//         }
//         return closest;
//     }

//     private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList) {
//         HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
//         foreach (var room in roomList) {
//             for (int col = offset; col < room.size.x - offset; col++) {
//                 for (int row = offset; row < room.size.y - offset; row++) {
//                     Vector2Int position = (Vector2Int) room.min + new Vector2Int(col, row);
//                     floor.Add(position);
//                 }
//             }
//         }
//         return floor;
//     }

// }
