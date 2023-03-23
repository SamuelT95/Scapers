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
public class Magical : Damage
{
    static float weaknessMod = 2.0f;
    static float resistanceMod = 2.0f;
    public MagicalType magicalType;

    internal override bool doAttack(Character caster, Character reciever)
    {
        float damage = caster.getModifiedDamage(Damage);

        if(reciever.magicalWeakness == magicalType)
        {
            damage *= weaknessMod;
        }

        if (reciever.magicalResistance == magicalType)
        {
            damage /= resistanceMod;
        }

        return reciever.Takedamage(damage);
    }
}


