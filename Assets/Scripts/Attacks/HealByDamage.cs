using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class HealByDamage : Physical
{
    internal override string doAttack(Character caster, Character reciever)
    {
        string message = base.doAttack(caster, reciever);

        float damage = caster.getModifiedHealth(Damage);
        caster.Heal(damage - reciever.getDefense());

        return message + "\n" + caster.Name + " was healed for " + damage.ToString();
    }

}
