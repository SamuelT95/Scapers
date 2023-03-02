using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // public attributes of characters, meant to be modified in unity
    public int level;
    public float baseMaxHealth;
    public float baseDefense;

    public Attack[] attacks = new Attack[4];

    // these will affect how much our power increases with levels
    private float healthModifier = 0.1f;
    private float damageModifier = 0.1f;
    private float defenseModifier = 0.1f;

    private float defense;
    private float health;
    private float maxHealth;

    
    //set the attributes of character based on their level
    void Start()
    {
        levelUp();
    }

    internal void levelUp()
    {
        maxHealth = baseMaxHealth;
        defense = baseDefense;

        //calculates what our stats should be at current level
        for(int i = 0; i < level; i++)
        {
            maxHealth = maxHealth + maxHealth * healthModifier;
            defense = defense + defense * defenseModifier;
        }

        health = maxHealth;
    }

    internal float getModifiedDamage(float damage, int level)
    {
        for (int i = 0; i < level; i++)
        {
            damage = damage + damage * damageModifier;
        }

        return damage;
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public bool Takedamage(Attack attack, int enemyLevel)
    {
        float rFloat = UnityEngine.Random.Range(0f, 1f);

        if (rFloat < attack.FailureRate)
        {
            return true;
        }

        health -= getModifiedDamage(attack.Damage, enemyLevel);

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

