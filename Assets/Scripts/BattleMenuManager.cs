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
    private GameObject DialogMenu;

    private string nextMessage;
    private int dialogState = 0;

    Player player;
    Character enemy;
    TextMeshProUGUI dialogText;

    public void Start()
    {
        //get all buttons
        Button attack = GameObject.Find("ButtonAttack").GetComponent<Button>();
        Button run = GameObject.Find("ButtonRun").GetComponent<Button>();
        Button heal = GameObject.Find("ButtonHeal").GetComponent<Button>();
        Button physical = GameObject.Find("ButtonPhysical").GetComponent<Button>();
        Button magical = GameObject.Find("ButtonMagic").GetComponent<Button>();
        Button next = GameObject.Find("ButtonNext").GetComponent<Button>();

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
        next.onClick.AddListener(Next);

        //get individual menus
        BattleMenu = GameObject.Find("BattleMenu");
        AttackMenu = GameObject.Find("AttackMenu");
        PhysicalAttackMenu = GameObject.Find("PhysicalAttackMenu");
        MagicAttackMenu = GameObject.Find("MagicalAttackMenu");
        DialogMenu = GameObject.Find("DialogMenu");

        dialogText = DialogMenu.GetComponentInChildren<TextMeshProUGUI>();

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
        DialogMenu.SetActive(false);
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

    public void useAttack(Attack attack, Character caster, Character reciever)
    {
        dialogText.text = caster.Name + " used " + attack.name;
        nextMessage = attack.tryAttack(caster, reciever);
    }

    public void PhysAttack(int buttonNo)
    {
        PhysicalAttackMenu.SetActive(false);
        DialogMenu.SetActive(true);
        useAttack(player.physicalAttacks[buttonNo], player, enemy);
    }

    public void MagAttack(int buttonNo)
    {
        MagicAttackMenu.SetActive(false);
        DialogMenu.SetActive(true);
        useAttack(player.magicalAttacks[buttonNo], player, enemy);
    }

    public void Next()
    {
        switch(dialogState)
        {
            case 0:
            case 2:
                dialogText.text = nextMessage;
                break;
            case 1:
                Attack attack = enemy.GetRandomAttack();
                dialogText.text = enemy.name + " used " + attack.name;
                useAttack(attack, enemy, player);
                break;
            case 3:
                dialogState = 0;
                DialogMenu.SetActive(false);
                BattleMenu.SetActive(true);
                return;
        }
        dialogState++;
    }


}
