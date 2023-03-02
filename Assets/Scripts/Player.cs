using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int exp = 0;

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
