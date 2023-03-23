using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuManager : MonoBehaviour
{

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
        BattleMenu.SetActive(true);
        AttackMenu.SetActive(false);
        PhysicalAttackMenu.SetActive(false);
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


}
