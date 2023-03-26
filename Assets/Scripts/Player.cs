using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THis classs is responsible for giving exp to the player
/// </summary>
public class Player : Character
{
    private int exp = 0;

    /// <summary>
    /// adds exp to the player and updates his level
    /// </summary>
    /// <param name="exp"></param>
    public void addExp(int exp)
    {
        this.exp += exp;

        //check if we gained a level
        if(exp > Mathf.Pow(level, 2) /2)
        {
            level = (int)Mathf.Sqrt(exp * 2);
            levelUp();
        }
    }
}
