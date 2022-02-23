using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectiles : MonoBehaviour{

    // -- shooting setting
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectilesPerBurst;
    [SerializeField] private GameObject projectile;
    
    private GameObject enemy;
    private EnemyStats stats;
    private GameObject target;
    private float cooldownTimer;    // -- shoot once every cooldown
    private float burstTimer;       // -- shoot once every burst cooldown
    private float shootWindow;      // -- only shoot during shoot window

    
    void Start(){
        projectilesPerBurst += 1; // -- hacky, always shoots 1 less than provided amount.
        enemy = this.gameObject;
        stats = enemy.GetComponent<EnemyStats>();
        target = GameObject.Find("Player");
        cooldownTimer = stats.getEnemyAttackSpeed();
        shootWindow = stats.getEnemyAttackSpeed() / 3;
        burstTimer = (stats.getEnemyAttackSpeed() / 3) / projectilesPerBurst;
    }

    void Update(){
        // -- try to shoot
        shoot();
    }

    // -- might have to change to IEnumerator
    private void shoot(){
        // -- kinda a hack, only one at a time.
        // -- cool > shoot > burst times
        // -- 5 >  1.66 > 0.222
        cooldownTimer -= Time.deltaTime;
        shootWindow -= Time.deltaTime;
        burstTimer -= Time.deltaTime;

        if(shootWindow > 0 && cooldownTimer > 0){
            if(burstTimer < 0){
                GameObject bulletObject = Instantiate(projectile, enemy.transform.position, Quaternion.Euler(0,0,30));
                EnemyProjectile bullet = bulletObject.GetComponent<EnemyProjectile>();
                bullet.setDamage(stats.getEnemyDamage());
                bullet.setTarget(target.transform.position);
                bullet.setProjectileSpeed(projectileSpeed);
                bullet.shoot();
                burstTimer = (stats.getEnemyAttackSpeed() / 3) / projectilesPerBurst;
            }
        }
        else if(cooldownTimer < 0){
            // -- reset timers for next attack
            cooldownTimer = stats.getEnemyAttackSpeed();
            shootWindow = stats.getEnemyAttackSpeed() / 3;
            burstTimer = (stats.getEnemyAttackSpeed() / 3) / projectilesPerBurst;
        }

    }
}
