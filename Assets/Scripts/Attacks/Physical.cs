using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhysicalType
{
    Slash,
    Blunt,
    Fighting,
    Stab,
    None
}

[CreateAssetMenu]
public class Physical : Damage
{
    static float weaknessMod = 2.0f;
    static float resistanceMod = 2.0f;
    public PhysicalType physicalType;

    internal override bool doAttack(Character caster, Character reciever)
    {
        float damage = caster.getModifiedDamage(Damage);

        if (reciever.physicalWeakness == physicalType)
        {
            damage *= weaknessMod;
        }

        if (reciever.physicalResistance == physicalType)
        {
            damage /= resistanceMod;
        }

        return reciever.Takedamage(caster.getModifiedDamage(Damage));
    }
}
