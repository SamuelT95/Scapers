using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Attack[] attacks = new Attack[4];

    public bool Takedamage(Attack attack)
    {
        float rFloat = UnityEngine.Random.Range(0f, 1f);

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

    public Attack GetRandomAttack()
    {
        int selection = UnityEngine.Random.Range(1, 5);
        return attacks[selection];
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

