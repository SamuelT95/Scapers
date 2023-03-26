using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// represnts a character that can enter battles in the world
/// </summary>
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

    /// <summary>
    /// Sets the players stats based on their level
    /// </summary>
    internal void levelUp()
    {
        maxHealth = baseMaxHealth;
        defense = baseDefense;

        //calculates what our stats should be at current level
        maxHealth = baseMaxHealth * Mathf.Pow(1 + healthModifier, level);
        defense = baseDefense * Mathf.Pow(1 + defenseModifier, level);

        health = maxHealth;
    }

    /// <summary>
    /// returns the damage based on the players level
    /// </summary>
    /// <param name="damage">the base damage</param>
    /// <returns></returns>
    internal float getModifiedDamage(float damage)
    {
        if(damage <= 0.0f)
        {
            return 0.0f;
        }
        damage = damage * Mathf.Pow(1 + damageModifier, level);

        return damage;
    }

    /// <summary>
    /// returns the health based on the palyeurs level
    /// </summary>
    /// <param name="amount"> the base health of the player</param>
    /// <returns></returns>
    internal float getModifiedHealth(float amount)
    {
        amount = amount * Mathf.Pow(1 + healthModifier, level);
        return amount;
    }


    /// <summary>
    /// returns the characters current health
    /// </summary>
    /// <returns></returns>
    public float getHealth()
    {
        return health;
    }

    /// <summary>
    /// returns the characters maxhealth
    /// </summary>
    /// <returns></returns>
    public float getMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Damages the character based on the specified amount
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool Takedamage(float damage)
    {
        if(defense > damage)
        {
            return true;
        }
        health -= damage - defense;
        return true;
    }

    /// <summary>
    /// Gets a random attack from the pool of attacks
    /// </summary>
    /// <returns></returns>
    public Attack GetRandomAttack()
    {
        Attack attack = null;

        List<Attack> attacks = new List<Attack>();
        attacks.AddRange(physicalAttacks);
        attacks.AddRange(magicalAttacks);

        int selection = UnityEngine.Random.Range(0, attacks.Capacity);

        attack = attacks[selection];

        while(attack == null )
        {
            selection++;
            if(attacks.Capacity > 0)
            {
                attack= attacks[selection % attacks.Capacity];
            }
            else
            {
                Physical struggle = ScriptableObject.CreateInstance<Physical>();
                struggle.Damage = 1;
                struggle.Name = "Struggle";
                struggle.FailureRate = 1;
                struggle.physicalType = PhysicalType.None;
                return struggle;
            }
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

