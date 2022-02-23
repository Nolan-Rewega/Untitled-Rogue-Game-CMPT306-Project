using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Default Move speed for the player
    public float MoveSpd = 5f;

    // Reference to the rigid body that acts as motor to move the player
    public Rigidbody2D rb;

    // A reference to the animator for direct changes input based on player inputs
    public Animator animator;

    // Store's x and y. Where, x = horizontal movement and y = vertical movement
    Vector2 Movement;

    private GameObject corridorFirstDungeonGenerator;

    public void EnterDungeon()
    {
        corridorFirstDungeonGenerator = GameObject.Find("CorridorFirstDungeonGenerator");
        Vector3Int startPosition = (Vector3Int) corridorFirstDungeonGenerator.GetComponent<CorridorFirstDungeonGenerator>().startRoomPosition;
        transform.position = startPosition;
    }


        // Update is called once per frame. Register's user inputs
        void Update()
    {
        // Input for player movement
        Movement.x = Input.GetAxisRaw("Horizontal"); // store x movement (horizontal movement)
        Movement.y = Input.GetAxisRaw("Vertical"); // store y movement (vertical movement)

        // Grab input and change float values in animator state
        animator.SetFloat("Horizontal", Movement.x);
        animator.SetFloat("Vertical", Movement.y);
        animator.SetFloat("Speed", Movement.sqrMagnitude); // some performance trick

        // add the last movement found, and update the idle state animation
        if (Movement.x == 1 || Movement.y == 1 || Movement.x == -1 || Movement.y == -1)
        {
            animator.SetFloat("LastMoveHorizontal", Movement.x);
            animator.SetFloat("LastMoveVertical", Movement.y);
        }

        // check input for walking feature. This would slow down/modify the walking speed of the player.
        // Fire3 = left shift key (alt key) - check edit-input managers for default settings.
        if (Input.GetButtonDown("Fire3"))
        {
            MoveSpd = 2f;

        } else if (Input.GetButtonUp("Fire3"))
        {
            MoveSpd = 5f;
        }

    }

    // Fixedupdate is same as update, that calls user inputs to the player
    private void FixedUpdate()
    {
        
        // Call Input / Movement to the rigidbody(Player)
        rb.MovePosition(rb.position + Movement * MoveSpd * Time.fixedDeltaTime);


    }
}
