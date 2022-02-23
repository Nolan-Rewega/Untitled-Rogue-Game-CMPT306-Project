using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour{

    // -- Enemy attributes
    private List<GameObject> enemies;
    [SerializeField] private List<GameObject> enemyPrefabs;
    private int enemiesPerRoom;

    // -- death marker attributes
    [SerializeField] private GameObject deathMarker;
    private Queue<GameObject> deaths; 
    private int maxNumberOfShowDeaths;

    // -- at start time
    void Start(){
        enemies = new List<GameObject>();
        deaths = new Queue<GameObject>();
        maxNumberOfShowDeaths = 20;
        enemiesPerRoom = 5;
        // -- add listeners
        RoomManager.current.onRoomEnter += spawnEnemiesInRoom;
    }

    // -- at update time.
    void Update(){
        // -- checking every frame not good. (OPTIMIZE THIS)
        if(enemies.Count > 0){
            for(int i = 0; i < enemies.Count; i++){
                if(enemies[i].GetComponent<EnemyStats>().getIsDead()) { destroyEnemy(enemies[i]); }
            }
        }
        else if(enemies.Count <= 0){ 
            RoomManager.current.enemiesDefeated();
        }
    }


    // -- should destroy a enemy and add a death marker
    private void destroyEnemy(GameObject enemy){
        if(enemies.Contains(enemy)){
            // -- adds a death marker
            var marker = Instantiate(deathMarker, enemy.transform.position, Quaternion.identity);
            if(deaths.Count == maxNumberOfShowDeaths){ 
                Destroy(deaths.Dequeue());
            }
            deaths.Enqueue(marker);

            // -- destroy enemy
            enemies.RemoveAt(enemies.IndexOf(enemy));
            Destroy(enemy);
        }
    }

    private void spawnEnemiesInRoom(Vector3 room_center){
        // -- we need a random place in the room
        Vector3 pos = new Vector3(0,0,0);

        for(int i = 0; i < enemiesPerRoom; i++){
            // -- scuffed implemntation for now lol
            int idx = Random.Range(0, enemyPrefabs.Count);
            Debug.Log(idx);
            pos.x = room_center.x + Random.Range(-7.0f, 7.0f);
            pos.y = room_center.y + Random.Range(-7.0f, 7.0f);

            var spawnedEnemy = Instantiate(enemyPrefabs[idx], pos, Quaternion.identity);
            enemies.Add(spawnedEnemy);
        }
    }

}
