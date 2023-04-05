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
    public int AddExp(int levelOfDefeated)
    {
        int expGained = (int)Mathf.Pow(levelOfDefeated, 3);
        this.exp += expGained;

        //check if we gained a level
        if(this.exp > Mathf.Pow(level, 3))
        {
            level = (int)Mathf.Sqrt(exp * 2);
            levelUp();
        }

        return expGained;
    }
}
