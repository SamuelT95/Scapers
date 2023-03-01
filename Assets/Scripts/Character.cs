using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    public int maxHealth;
    internal List<Attack> _attacks;


    public List<Attack> Attacks { get { return _attacks; }}


    public bool Takedamage(Attack attack)
    {
        float rFloat = Random.Range(0f, 1f);

        if (rFloat < attack.FailureRate)
        {
            return true;
        }

        health -= attack.Damage;

        if (health <= 0)
        {
            return false;
        }

        return true;
    }

    public void Heal(int amount)
    {
        while(health < maxHealth && amount != 0) 
        {
            health++;
            amount--;
        }
    }
        
}

