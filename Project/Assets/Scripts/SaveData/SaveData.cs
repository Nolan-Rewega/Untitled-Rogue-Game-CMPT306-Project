using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public float playerHealth;
    public float attackStrength;
    public float playerSpeed;
    public GameObject[] enemies;
    public GameObject[] itemsOnGround;
    // [SerializedField]
    // public List<Tile> dungeonTiles;
    public GameObject[] playerItems;

    public string ToJSON(){return JsonUtility.ToJson(this);}

    public void LoadFromJSON(string fileName){JsonUtility.FromJsonOverwrite(fileName, this);}
}

public interface Savable{
    void PopulateSaveData(SaveData sd);
    void LoadFromSaveData(SaveData sd);
}
