using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Attack
{
    internal string _name;
    internal int _damage;
    internal float _failureRate;

    public string Name { get { return _name; } }
    public int Damage { get { return _damage; } }
    public float FailureRate { get { return _failureRate; } }
}
