using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MagicalType
{
    Fire,
    Water,
    Air,
    Electric,
    None
}

[CreateAssetMenu]
public class Magical : Attack
{
    static float weaknessMod = 2.0f;
    static float resistanceMod = 2.0f;
    public MagicalType magicalType;

    internal override string doAttack(Character caster, Character reciever)
    {
        
        float damage = caster.getModifiedDamage(Damage);

        int effective = 1;

        if(reciever.magicalWeakness == magicalType)
        {
            damage *= weaknessMod;
            effective += 1;
        }

        if (reciever.magicalResistance == magicalType)
        {
            damage /= resistanceMod;
            effective -= 1;
        }

        string response = reciever.Name + " was damaged for " + damage.ToString();

        switch(effective)
        {
            case 0: response += ". It was not very effective"; 
                break;
            case 1: response += ". It was effective";
                break;
            case 2: response += ". It was super effective!";
                break;
        }

        reciever.Takedamage(damage);

        return response;
    }
}


