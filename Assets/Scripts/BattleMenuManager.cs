using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuManager : MonoBehaviour
{
/*    GameObject player = GameObject.FindGameObjectWithTag("Player");
    GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");*/

    public GameObject BattleMenu;
    public GameObject AttackMenu;
    public GameObject PhysicalAttackMenu;
    public GameObject MagicAttackMenu;

    public void Attack()
    {
        BattleMenu.SetActive(false);
        AttackMenu.SetActive(true);
    }

    public void Back()
    {
        if (BattleMenu)
        {
            BattleMenu.SetActive(true);
        }
        if (AttackMenu)
        {
            AttackMenu.SetActive(false);
        }
        if (PhysicalAttackMenu)
        {
            PhysicalAttackMenu.SetActive(false);
        }
        if (MagicAttackMenu)
        {
            MagicAttackMenu.SetActive(false);
        }
    }

    public void Heal()
    {
        Debug.Log("You healed");
    }

    public void Run()
    {
        Debug.Log("You ran");
    }

    public void Physical()
    {
        AttackMenu.SetActive(false);
        PhysicalAttackMenu.SetActive(true);
    }

    public void Slash()
    {
        Debug.Log("Slashing damage");
        
        Back();
    }

    public void Crush()
    {
        Debug.Log("Crushing damage");
        Back();
    }

    public void Pierce()
    {
        Debug.Log("Piercing damage");
        Back();
    }

    public void Magical()
    {
        AttackMenu.SetActive(false);
        MagicAttackMenu.SetActive(true);
    }

    public void Air()
    {
        Debug.Log("Air damage");
        Back();
    }
    public void Electric()
    {
        Debug.Log("Electric damage");
        Back();
    }

    public void Water()
    {
        Debug.Log("Water damage");
        Back();
    }

    public void Fire()
    {
        Debug.Log("Fire damage");
        Back();
    }


}
