using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset = new Vector3(0, 0, -10);

    [SerializeField]
    private float player_camera_difference;

    [SerializeField]
    private int cameraFloatX = 3;

    [SerializeField]
    private int cameraFloatY = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnterDungeon() {
        // Nathan edited November 3rd
        // player = GameObject.Find("Player");
        // transform.position = player.transform.position + offset;
        // player.GetComponent<PlayerMovement>().EnterDungeon();

        transform.position = new Vector3Int(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        float player_camera_differenceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float player_camera_differenceY = Mathf.Abs(player.transform.position.y - transform.position.y);
        if (player_camera_differenceX > cameraFloatX) {
            if (player.transform.position.x > transform.position.x) {
                transform.position = new Vector3(player.transform.position.x - cameraFloatX, transform.position.y, -10);
            } else {
                transform.position = new Vector3(player.transform.position.x + cameraFloatX, transform.position.y, -10);
            }
        }
        if (player_camera_differenceY > cameraFloatY) {
            if (player.transform.position.y > transform.position.y) {
                transform.position = new Vector3(transform.position.x, player.transform.position.y - cameraFloatY, -10);
            } else {
                transform.position = new Vector3(transform.position.x, player.transform.position.y + cameraFloatY, -10);
            }
        }
    }
}
