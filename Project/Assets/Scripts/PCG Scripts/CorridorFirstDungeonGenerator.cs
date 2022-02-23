using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator {
    
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5, corridorMultiplier = 5;

    private float roomPercent = 1f;

    public Vector2Int startRoomPosition, bossRoomPosition, shopRoomPosition, itemRoomPosition, arenaRoomPosition;

    private GameObject player;

    private GameObject Boss;

    [SerializeField]
    private HashSet<Vector2Int> floorMap;
    // private List<Vector2Int> floorMap;

    public List<Vector2Int> allRoomCentres;

    public Dictionary<Vector2Int, HashSet<Vector2Int>> roomHashSetDictionary;
    public Dictionary<Vector2Int, Vector2Int[,]> roomArrayDictionary;



    public GameObject T, R, B, L, TR, TB, TL, RB, RL, BL, TRB, TRL, TBL, RBL, TRBL;

    public GameObject forceField;


    // -- change to Awake() time from Start()  ~ Nolan nov
    void Awake(){
        GenerateDungeon();
    }


    private void DoorSpawn(List<Vector2Int> roomCentres) { 
        foreach (var roomCentre in roomCentres) {


            if (roomCentre == startRoomPosition) {
                Instantiate(forceField, new Vector3(roomCentre.x, roomCentre.y, 0), Quaternion.identity);
            }
            

            if (roomCentre != startRoomPosition && roomCentre != shopRoomPosition) {
                var right = new Vector2Int(roomCentre.x + 16, roomCentre.y);
                var left = new Vector2Int(roomCentre.x - 16, roomCentre.y);
                var top = new Vector2Int(roomCentre.x, roomCentre.y + 16);
                var bottom = new Vector2Int(roomCentre.x, roomCentre.y - 16);

                var doorChoice = DoorChooser(top, right, left, bottom);

                Instantiate(doorChoice, new Vector3(roomCentre.x, roomCentre.y, 0), Quaternion.identity);
            

            }
        }
    }

    private GameObject DoorChooser(Vector2Int top, Vector2Int right, Vector2Int left, Vector2Int bottom) {

        if (floorMap.Contains(top)) {               // contains top...
            if (floorMap.Contains(right)) {         // contains top and right...
                if (floorMap.Contains(bottom)) {    // contains top, right, and bottom...
                    if (floorMap.Contains(left)) {  // contains all!
                        // TRBL 1111
                        return TRBL;
                    } else {                        // contains top, right, bottom!
                        // TRB 1110
                        return TRB;
                    }
                } else {    // contains top, right, but not bottom...
                    if (floorMap.Contains(left)) {  // contains top, right, left!
                        // TRL 1101
                        return TRL;
                    } else {    // contains top, right!
                        // TR 1100
                        return TR;
                    }
                }
            } else {    // contains top but not right...
                if (floorMap.Contains(bottom)) {    // contains top, bottom...
                    if (floorMap.Contains(left)) {  // contains top, bottom, left!
                        // TBL 1011
                        return TBL;
                    } else {    // contains top, bottom!
                        // TB 1010
                        return TB;
                    }
                } else {    // contains top, not right, not bottom...
                    if (floorMap.Contains(left)) {  // contains top, left!
                        // TL 1001
                        return TL;
                    } else {    // contains top!
                        // T 1000
                        return T;
                    }
                }
            }
        } 
        else {    // does not contain top
            if (floorMap.Contains(right)) {         // contains right...
                if (floorMap.Contains(bottom)) {    // contains right and bottom...
                    if (floorMap.Contains(left)) {  // contains right, bottom, left!
                        // RBL 0111
                        return RBL;
                    } else {    // contains right, bottom!
                        // RB 0110
                        return RB;
                    }
                } else {    // contains right, no bottom...
                    if (floorMap.Contains(left)) {  // contains right, left!
                        // RL 0101
                        return RL;
                    } else {    // contains right
                        // R 0100
                        return R;
                    }
                }
            }

            else {  // does not contain right
                if (floorMap.Contains(bottom)) {    // contains bottom...
                    if (floorMap.Contains(left)) {  // contains bottom, left!
                        // BL 0011
                        return BL;
                    } else {    // contains bottom!
                        // B 0010
                        return B;
                    }
                } else {    // contains left
                    // L 0001
                    return L;
                } 
            }
        }
    }


    public HashSet<Vector2Int> GetFloorPositions() {
        return floorMap;
    }

    private void SpawnPlayer() {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().EnterDungeon();
    }

    private void SpawnBoss() {  
        Boss = GameObject.Find("Boss");
        Boss.GetComponent<BossSpawn>().Spawn();
    }

    protected override void RunProceduralGeneration() {
        CorridorFirstGeneration();
    }









    private void CorridorFirstGeneration() {
        // create dictionaries
        roomHashSetDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
        roomArrayDictionary = new Dictionary<Vector2Int, Vector2Int[,]>();
        
        // set all rooms to the origin
        startRoomPosition = new Vector2Int(0, 0);
        shopRoomPosition = new Vector2Int(0, 0);
        bossRoomPosition = new Vector2Int(0, 0);
        itemRoomPosition = new Vector2Int(0, 0);
        arenaRoomPosition = new Vector2Int(0, 0);

        SpawnPlayer();


        // delete previous doors
        GameObject[] allDoors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in allDoors) {
            DestroyImmediate(door);
        }

        GameObject[] forceFieldDelete = GameObject.FindGameObjectsWithTag("Force Field");
        foreach (GameObject field in forceFieldDelete) {
            DestroyImmediate(field);
        }

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);
        allRoomCentres = potentialRoomPositions.ToList();

        // List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        // foreach(var room in deadEnds) {
        //     allRoomCentres.Add(room);
        // }


        // choose boss room
        // FindFarthestRoom(deadEnds);
        // if (bossRoomPosition == startRoomPosition) {
        FindFarthestRoom(allRoomCentres);
        // }
        SpawnBoss();

        // choose random room to be the shopRoom
        while (shopRoomPosition == startRoomPosition || shopRoomPosition == bossRoomPosition) {
            shopRoomPosition = allRoomCentres[Random.Range(0, allRoomCentres.Count)];
        }

        // choose random room to be the itemRoom
        while (itemRoomPosition == startRoomPosition || itemRoomPosition == bossRoomPosition || itemRoomPosition == shopRoomPosition) {
            itemRoomPosition = allRoomCentres[Random.Range(0, allRoomCentres.Count)];
        }

        // choose random room to be arenaRoom
         while (arenaRoomPosition == startRoomPosition || arenaRoomPosition == bossRoomPosition || arenaRoomPosition == shopRoomPosition arenaRoomPosition == itemRoomPosition) {
            arenaRoomPosition = allRoomCentres[Random.Range(0, allRoomCentres.Count)];
        }



        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        

        // CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);



        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);


        floorMap = floorPositions;
        // floorMap = floorPositions.ToList<Vector2Int>();

        DoorSpawn(allRoomCentres);

        CreateRoomArrayDictionary();

        

        // foreach (var room in roomArrayDictionary.Keys) {
        //     var test = roomArrayDictionary[room];
        //     for (int y = 0; y < 49; y++) {
        //         for (int x = 0; x < 49; x++) {
        //             Debug.Log(test[y, x]);
        //         }
        //     }
        //     break;
        // }

    }

    private void CreateRoomArrayDictionary() {
        var dict = roomHashSetDictionary;
        foreach (var room in dict.Keys) {
            Vector2Int[,] roomArray = new Vector2Int[49, 49];
            foreach (var tile in dict[room]) {
                // get the difference between the current position and the centre of 
                int xDiff = tile.x - room.x;
                int yDiff = room.y - tile.y;

                int row_Index = 24 + xDiff;
                int col_Index = 24 + yDiff;
                roomArray[col_Index, row_Index] = tile;
            }
            roomArrayDictionary.Add(room, roomArray);
        }
    }

    // private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) {
        // var farthestRoomPosition = new Vector2Int(0, 0);
        // float farthestRoomDistance = 0;
        // foreach (var position in deadEnds) {
            // float currentRoomDistance = Mathf.Sqrt(Mathf.Pow(startRoomPosition.x - position.x, 2f) + Mathf.Pow(startRoomPosition.y - position.y, 2f));

            // if (currentRoomDistance > farthestRoomDistance) {
            //     farthestRoomDistance = currentRoomDistance;
            //     farthestRoomPosition = position;
            // }

            // if (!roomFloors.Contains(position)) {
            //     var room = RunRandomWalk(basicRoom, position);
            //     roomFloors.UnionWith(room);
            // }
        // }
        // if (farthestRoomPosition != startRoomPosition && farthestRoomPosition != shopRoomPosition) {
        //     SpawnBoss(farthestRoomPosition);
        // } else {
        //     Debug.Log("Same position");
        //     findFarthestRoom();
        // }
    // }

    // Sets boss room position to furthest room in the list from the start
    private void FindFarthestRoom(List<Vector2Int> rooms){
        bossRoomPosition = startRoomPosition;
        float farthestRoomDistance = 0;
        foreach (var currentRoom in rooms) {
            float currentRoomDistance = Mathf.Sqrt(Mathf.Pow(startRoomPosition.x - currentRoom.x, 2f) + Mathf.Pow(startRoomPosition.y - currentRoom.y, 2f));

            if (currentRoomDistance > farthestRoomDistance) {
                farthestRoomDistance = currentRoomDistance;
                bossRoomPosition = currentRoom;
            }
        }    
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions) {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var position in floorPositions) {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList) {
                if (floorPositions.Contains(position + direction)) {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1) {
                    deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions) {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => System.Guid.NewGuid()).Take(roomToCreateCount).ToList();



        foreach (var roomPosition in roomsToCreate) {
            if (roomPosition == shopRoomPosition) {
                var roomFloor = RunRandomWalk(shopRoom, roomPosition);
                roomPositions.UnionWith(roomFloor);
                roomHashSetDictionary.Add(roomPosition, roomFloor);
            } else {
                var roomFloor = RunRandomWalk(basicRoom, roomPosition);
                roomPositions.UnionWith(roomFloor);
                roomHashSetDictionary.Add(roomPosition, roomFloor);
            }
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        var temp = corridorMultiplier;
        do {
            currentPosition = startPosition;
            for (int x = 0; x < corridorCount; x++) {
                var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                currentPosition = corridor[corridor.Count - 1];
                potentialRoomPositions.Add(currentPosition);
                floorPositions.UnionWith(corridor);
            }
            temp--;
        } while (temp > 0);
    }
}

