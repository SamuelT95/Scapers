using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Attack
{
    Punch()
    {
        _name = "Punch";
        _damage = 10;
        _failureRate = 0.1f;
    }

}
