using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Attack : MonoBehaviour
{

    public Animator animator;

    Vector2 movement;

    // for bullet prefab
    public GameObject bulletPrefab;

    // for controlling bullet/projectile speed
    private float bulletSpeed = 10.0f;

    // for bullet firing delay
    private float lastFire;
    private float fireDelay;

    private PlayerListener PL;

    private void Start()
    {
        PL = GetComponent<PlayerListener>();
        fireDelay = 1 / PL.getPlayerAttackSpeed();
        Debug.Log(fireDelay);

    }


    // Update is called once per frame
    void Update()
    {
        
        // Input for player movement
        movement.x = Input.GetAxisRaw("ShootHorizontal"); // store x movement (horizontal movement)
        movement.y = Input.GetAxisRaw("ShootVertical"); // store y movement (vertical movement)

        float lastMovex = movement.x;
        float lastMovey = movement.y;

        // check if space bar input has been pressed, then call attack() animation.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();

        } else if(lastMovex != 0 || lastMovey != 0)
        {
            if (Time.time > lastFire + fireDelay)
            {
                // Set trigger to play the Shoot animation
                animator.SetTrigger("Shoot");
                Shoot(lastMovex, lastMovey);
                lastFire = Time.time;
            }
        }

        
    }

    void Shoot(float x, float y)
    {

        // add the last movement found, and update the idle state animation
        if (x == 1 || y == 1 || x == -1 || y == -1)
        {
            animator.SetFloat("LastMoveHorizontal", x);
            animator.SetFloat("LastMoveVertical", y);
        }


        // NOTE: POSITION ARE AUGMENTED SO THE SPAWN POINT OF THE BULLET DO NOT DIRECTLY HIT THE PLAYER AND DISAPPEAR
        if (x < 0)
        {
            // The use of this ternary operators is taken from:
            // https://www.youtube.com/watch?v=EWo3tAG-iAg&ab_channel=Chillehh
            // this would rotate prefab to the left
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(-0.7f, 0, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.BulletDmg(PL.getPlayerDamage());
            bc.SetLifetime(PL.getPlayerRange());
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0; // we wanna have no gravity for bullet shooting
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
                );

        }
        else if (x > 0)
        {
            // The use of this ternary operators is taken from:
            // https://www.youtube.com/watch?v=EWo3tAG-iAg&ab_channel=Chillehh
            // this would rotate prefab to the right
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0.7f, 0, 0), Quaternion.Euler(0, 0, -90)) as GameObject;
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.BulletDmg(PL.getPlayerDamage());
            bc.SetLifetime(PL.getPlayerRange());
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0; // we wanna have no gravity for bullet shooting
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
                );

        }
        else if (y < 0)
        {

            // The use of this ternary operators is taken from:
            // https://www.youtube.com/watch?v=EWo3tAG-iAg&ab_channel=Chillehh
            // this would rotate prefab down
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, -0.9f, 0), Quaternion.Euler(0, 0, 180)) as GameObject;
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.BulletDmg(PL.getPlayerDamage());
            bc.SetLifetime(PL.getPlayerRange());
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0; // we wanna have no gravity for bullet shooting
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
                );

        }
        else if (y > 0)
        {
            // The use of this ternary operators is taken from:
            // https://www.youtube.com/watch?v=EWo3tAG-iAg&ab_channel=Chillehh
            // this would rotate prefab up
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.BulletDmg(PL.getPlayerDamage());
            bc.SetLifetime(PL.getPlayerRange());
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0; // we wanna have no gravity for bullet shooting
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
                );
        }




    }

    void Attack()
    {

        // Set trigger to play the animation attack
        animator.SetTrigger("Attack");




    }

}
