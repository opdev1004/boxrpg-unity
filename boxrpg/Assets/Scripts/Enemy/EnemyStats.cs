using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    int maxHealth;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 3;
        health = maxHealth;
    }

    //adds the specified amount of health
    public void AddHealth(int amount)
    {
        health += amount;

        if (health <= 0)
        {
            Debug.Log("Enemy " + gameObject.name + " was defeated.");
            Destroy(gameObject);
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }

        Debug.Log("Enemy Health set to " + health);
    }
}
