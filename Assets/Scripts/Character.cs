using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // public attributes of characters, meant to be modified in unity
    public string Name;
    public int level;
    public float baseMaxHealth;
    public float baseDefense;

    public Physical[] physicalAttacks = new Physical[4];
    public Magical[] magicalAttacks = new Magical[4];
    public PhysicalType physicalWeakness;
    public MagicalType magicalWeakness;
    public PhysicalType physicalResistance;
    public MagicalType magicalResistance;

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
        maxHealth = baseMaxHealth * Mathf.Pow(1 + healthModifier, level);
        defense = baseDefense * Mathf.Pow(1 + defenseModifier, level);

        health = maxHealth;
    }

    internal float getModifiedDamage(float damage)
    {
        damage = damage * Mathf.Pow(1 + damageModifier, level);

        return damage;
    }

    internal float getModifiedHealth(float amount)
    {
        amount = amount * Mathf.Pow(1 + healthModifier, level);
        return amount;
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public bool Takedamage(float damage)
    {
        health -= damage - defense;
        return true;
    }

    public Attack GetRandomAttack()
    {
        int type = UnityEngine.Random.Range(0, 1);
        int selection = UnityEngine.Random.Range(0, 4);

        Attack attack = null;

        if(type == 0)
        {
            attack = physicalAttacks[selection];
        }
        else
        {
            attack = magicalAttacks[selection];
        }

        if(attack == null)
        {
            attack = GetRandomAttack();
        }

        return attack;
    }

    public bool Heal(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        return true;
    }
        
}

