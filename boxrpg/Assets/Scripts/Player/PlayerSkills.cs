using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public GameObject projectileObj;
    Rigidbody rigidbody;
    private List<KeyCode> projectileSkillKey = new List<KeyCode> { KeyCode.Z, KeyCode.JoystickButton2 };

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode key in projectileSkillKey)
        {
            if (Input.GetKeyDown(key))
            {
                FireProjectile();
            }
        }
    }

    void FireProjectile()
    {
        GameObject projectileObject = Instantiate(projectileObj, rigidbody.position + Vector3.forward, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(Vector3.forward, 900);
    }
}
