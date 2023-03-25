using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Heal : Attack
{
    internal override string doAttack(Character caster, Character reciever)
    {
        float damage = caster.getModifiedHealth(Damage);
        caster.Heal(damage);

        return caster.Name + " was healed for " + damage.ToString();
    }
}
