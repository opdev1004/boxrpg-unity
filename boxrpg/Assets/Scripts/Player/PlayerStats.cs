using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles storing and changing player stats
public class PlayerStats : MonoBehaviour
{
    int health;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    //adds the specified amount of health
    public void AddHealth(int amount)
    {
        health += amount;
    }
}
