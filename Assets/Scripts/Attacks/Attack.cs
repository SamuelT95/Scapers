using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class Attack : ScriptableObject
{
    public string Name;
    public float Damage;
    public float FailureRate;

    //animation
    // etc.

    internal abstract bool doAttack(Character caster, Character reciever);

    public bool tryAttack(Character caster, Character reciever)
    {
        float rFloat = UnityEngine.Random.Range(0f, 1f);

        if (rFloat < FailureRate)
        {
            return false;
        }
        else
        {
            return doAttack(caster, reciever);
        }
    }
}
