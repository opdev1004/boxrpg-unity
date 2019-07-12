using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile Collision with " + collision.gameObject);
        Destroy(gameObject);
    }
 
    public void Launch(Vector3 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }
}
