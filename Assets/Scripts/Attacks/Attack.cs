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

    internal abstract string doAttack(Character caster, Character reciever);

    public string tryAttack(Character caster, Character reciever)
    {
        float rFloat = UnityEngine.Random.Range(0f, 1f);

        if (rFloat < FailureRate)
        {
            return "The attack missed!";
        }
        else
        {
            return doAttack(caster, reciever);
        }
    }
}
