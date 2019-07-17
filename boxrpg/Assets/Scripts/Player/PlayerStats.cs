using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles storing and changing player stats
public class PlayerStats : MonoBehaviour
{
    public int damageModifier {get; private set;} //damage dealt by skills

    int maxHealth;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        health = maxHealth;
        damageModifier = 1;

        //Initialises the hp text display
        UIHealthBar.instance.SetTextDisplay(health, maxHealth);
    }

    //adds the specified amount of health
    public void AddHealth(int amount)
    {
        health += amount;

        if (health < 0)
        {
            health = 0;
        } else if (health > maxHealth)
        {
            health = maxHealth;
        }

        UIHealthBar.instance.SetValue(health, maxHealth);
        Debug.Log("Health set to " + health);
    }
}
