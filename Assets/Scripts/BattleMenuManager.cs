using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuManager : MonoBehaviour
{
    
    //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");*/

    private GameObject BattleMenu;
    private GameObject AttackMenu;
    private GameObject PhysicalAttackMenu;
    private GameObject MagicAttackMenu;

    Player player;
    Character enemy;

    public void Start()
    {
        //get all buttons
        Button attack = GameObject.Find("ButtonAttack").GetComponent<Button>();
        Button run = GameObject.Find("ButtonRun").GetComponent<Button>();
        Button heal = GameObject.Find("ButtonHeal").GetComponent<Button>();
        Button physical = GameObject.Find("ButtonPhysical").GetComponent<Button>();
        Button magical = GameObject.Find("ButtonMagic").GetComponent<Button>();

        GameObject[] physButtons = GameObject.FindGameObjectsWithTag("physical_buttons");
        GameObject[] magButtons = GameObject.FindGameObjectsWithTag("magical_buttons");
        GameObject[] backButtons = GameObject.FindGameObjectsWithTag("back_buttons");

        //initialise our player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();

        //assign functions for single buttons
        attack.onClick.AddListener(Attack);
        run.onClick.AddListener(Run);
        heal.onClick.AddListener(Heal);
        physical.onClick.AddListener(Physical);
        magical.onClick.AddListener(Magical);

        //get individual menus
        BattleMenu = GameObject.Find("BattleMenu");
        AttackMenu = GameObject.Find("AttackMenu");
        PhysicalAttackMenu = GameObject.Find("PhysicalAttackMenu");
        MagicAttackMenu = GameObject.Find("MagicalAttackMenu");


        //set listeners for back buttons
        for (int i = 0; i < backButtons.Length; i++)
        {
            backButtons[i].GetComponent<Button>().onClick.AddListener(Back);
        }

        //set text of attack buttons and apply listeners
        for (int i = 0; i < physButtons.Length; i++)
        {
            //set text of buttons to their respective names
            magButtons[i].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = player.magicalAttacks[i].name;
            physButtons[i].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = player.physicalAttacks[i].name;

            //if you use i directly, it will create a reference of i and when the function is called later it will always be the last value it was
            int num = i;

            magButtons[i].GetComponent<Button>().onClick.AddListener(() => MagAttack(num));
            physButtons[i].GetComponent<Button>().onClick.AddListener(() => PhysAttack(num));
        }

        //disable all but one menu
        AttackMenu.SetActive(false);
        PhysicalAttackMenu.SetActive(false);
        MagicAttackMenu.SetActive(false);
    }

    public void Attack()
    {
        BattleMenu.SetActive(false);
        AttackMenu.SetActive(true);
    }

    public void Back()
    {
        if (AttackMenu.activeSelf)
        {
            AttackMenu.SetActive(false);
            BattleMenu.SetActive(true);
        }
        if (PhysicalAttackMenu.activeSelf)
        {
            PhysicalAttackMenu.SetActive(false);
            AttackMenu.SetActive(true);
        }
        if (MagicAttackMenu.activeSelf)
        {
            MagicAttackMenu.SetActive(false);
            AttackMenu.SetActive(true);
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

    public void Magical()
    {
        AttackMenu.SetActive(false);
        MagicAttackMenu.SetActive(true);
    }

    public void PhysAttack(int buttonNo)
    {
        print(player.physicalAttacks[buttonNo].name);
    }

    public void MagAttack(int buttonNo)
    {
        print(player.magicalAttacks[buttonNo].name);
    }
}
