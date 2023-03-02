using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Attack : ScriptableObject
{
    public string Name;
    public int Damage;
    public float FailureRate;
}
