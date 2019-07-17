using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public GameObject projectileObj;
    Rigidbody rigidbody;

    //Activates Projectile skill when pressing Z on the keyboard or X on the Gamepad.
    private List<KeyCode> projectileSkillKey = new List<KeyCode> { KeyCode.Z, KeyCode.JoystickButton2 };
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if projectile skill key is pressed.
        foreach (KeyCode key in projectileSkillKey)
        {
            if (Input.GetKeyDown(key))
            {
                FireProjectile(900);
            }
        }

        /*test code
        if (Input.GetKeyDown(KeyCode.T))
        {
            stats.AddHealth(-1);
        }*/
    }

    //Fires a projectile at at the specified speed.
    void FireProjectile(float force)
    {
        //Creates the projectile and renders it in the game.
        GameObject projectileObject = Instantiate(projectileObj, rigidbody.position + Vector3.forward, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(Vector3.forward, force, stats.damageModifier);
    }
}
