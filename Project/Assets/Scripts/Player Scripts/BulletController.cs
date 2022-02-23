using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private float Lifetime;
    private float ProjectileDmg;
    public GameObject hitEffect;

    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(VanishDelay());
        Destroy(gameObject, Lifetime);
    }



    /*
    IEnumerator VanishDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);


    }
    */

    void OnCollisionEnter2D(Collision2D collider)
    {   
        if(collider.gameObject.layer == 3){
            Debug.Log("testset");
            EnemyStats stats = collider.gameObject.GetComponent<EnemyStats>();
            Debug.Log(stats.getEnemyCurHealth());
            stats.modEnemyCurrHealth(-ProjectileDmg);

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
         if (collider.gameObject.name != "Player" && collider.gameObject.name != gameObject.name)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
    }

    public void SetLifetime(float lifetime)
    {
        Lifetime = lifetime;
    }

    public void BulletDmg(float dmg)
    {
        ProjectileDmg = dmg;
    }










}
