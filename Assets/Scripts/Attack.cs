using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private string _name;
    private int _damage;
    private int _failurRate;

    public string Name { get { return _name; } }
    public int Damage { get { return _damage; } }
    public int FailureRate { get { return _failurRate; } }

    Attack(string name, int damage, int failureRate)
    {
        _name = name;
        _damage = damage;
        _failurRate = failureRate;
    }
}
