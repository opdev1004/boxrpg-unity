using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidbody;

    float projectileLiveTime; //time until projectile is destroyed.
    int damageModifier;

    //Contrary to Start, Awake is called immediately when the object is created (when Instantiate is called), so rigidbody is properly initialized before being used.
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        projectileLiveTime = 1.0f;
        damageModifier = 0;
    }

    //runs every frame
    void Update()
    {
        //if projectile does not collide with anything after the specified time, destroy it.
        if (projectileLiveTime < 0)
        {
            Destroy(gameObject);
        } else
        {
            projectileLiveTime -= Time.deltaTime;
        }
        
    }

    //Action to take on collision with an object
    private void OnCollisionEnter(Collision collision)
    {
        if (damageModifier != 0)
        {
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();

            //deal damage to enemies with stats.
            if (enemyStats != null)
            {
                enemyStats.AddHealth(-damageModifier);
                Debug.Log("Projectile Collision with " + collision.gameObject + " for " + damageModifier + " damage.");
            }
        }
        Destroy(gameObject);
    }
 
    //Moves the projectile at the specified speed, direction, and power.
    public void Launch(Vector3 direction, float force, int power)
    {
        damageModifier = power;
        rigidbody.AddForce(direction * force);
    }
}
