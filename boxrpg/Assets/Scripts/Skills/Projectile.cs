using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidbody;

    float projectileLiveTime; //time until projectile is destroyed.

    //Contrary to Start, Awake is called immediately when the object is created (when Instantiate is called), so rigidbody is properly initialized before being used.
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        projectileLiveTime = 1.0f;
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
        Debug.Log("Projectile Collision with " + collision.gameObject);
        Destroy(gameObject);
    }
 
    //Moves the projectile at the specified speed and direction.
    public void Launch(Vector3 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }
}
