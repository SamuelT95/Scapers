using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Heal : Attack
{
    internal override bool doAttack(Character caster, Character reciever)
    {
        return caster.Heal(caster.getModifiedHealth(Damage));
    }
}
