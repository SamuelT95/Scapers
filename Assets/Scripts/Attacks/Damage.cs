using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Damage : Attack
{
    internal override bool doAttack(Character caster, Character reciever)
    {
        return reciever.Takedamage(caster.getModifiedDamage(Damage));
    }
}
