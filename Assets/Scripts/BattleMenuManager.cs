using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the menus with a batlle scene
/// </summary>
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

    /// <summary>
    /// Called once, basically an intializer
    /// </summary>
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
            int num = i;
            if (player.physicalAttacks[i] == null)
            {
                physButtons[i].SetActive(false);
            }
            else
            {
                physButtons[i].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = player.physicalAttacks[i].name;
                physButtons[i].GetComponent<Button>().onClick.AddListener(() => PhysAttack(num));
            }

            if (player.magicalAttacks[i] == null)
            {
                magButtons[i].SetActive(false);
            }
            else
            {
                magButtons[i].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = player.magicalAttacks[i].name;
                magButtons[i].GetComponent<Button>().onClick.AddListener(() => MagAttack(num));
            }

        }

        //disable all but one menu
        AttackMenu.SetActive(false);
        PhysicalAttackMenu.SetActive(false);
        MagicAttackMenu.SetActive(false);
        DialogMenu.SetActive(false);
    }

    /// <summary>
    /// disables and enables appropriate states when button pushed
    /// </summary>
    public void Attack()
    {
        BattleMenu.SetActive(false);
        AttackMenu.SetActive(true);
    }


    /// <summary>
    /// disables and enables appropriate states when back button pushed
    /// </summary>
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
        dialogText.text = "You ran away!";
        dialogState = 4;
        BattleMenu.SetActive(false);
        DialogMenu.SetActive(true);
        BattleManager.Instance.isBattleOver = true;
        BattleManager.Instance.isGameOver = false;
        BattleManager.Instance.enemySurvived = true;
        Debug.Log("You ran");
    }


    /// <summary>
    /// disables and enables appropriate states when button pushed
    /// </summary>
    public void Physical()
    {
        AttackMenu.SetActive(false);
        PhysicalAttackMenu.SetActive(true);
    }


    /// <summary>
    /// disables and enables appropriate states when button pushed
    /// </summary>
    public void Magical()
    {
        AttackMenu.SetActive(false);
        MagicAttackMenu.SetActive(true);
    }

    /// <summary>
    /// will execute an attack and store the return message for display
    /// </summary>
    /// <param name="attack">The attack to use</param>
    /// <param name="caster">The character that used the attack</param>
    /// <param name="reciever">the character that will be attacked</param>
    public void useAttack(Attack attack, Character caster, Character reciever)
    {
        dialogText.text = caster.Name + " used " + attack.name;
        nextMessage = attack.tryAttack(caster, reciever);
    }

    /// <summary>
    /// called when the player uses an attack
    /// </summary>
    /// <param name="buttonNo">The number of the button that was pressed</param>
    public void PhysAttack(int buttonNo)
    {
        PhysicalAttackMenu.SetActive(false);
        DialogMenu.SetActive(true);
        useAttack(player.physicalAttacks[buttonNo], player, enemy);
    }


    /// <summary>
    /// called when the player uses an attack
    /// </summary>
    /// <param name="buttonNo">The number of the button that was pressed</param>
    public void MagAttack(int buttonNo)
    {
        MagicAttackMenu.SetActive(false);
        DialogMenu.SetActive(true);
        useAttack(player.magicalAttacks[buttonNo], player, enemy);
    }

    /// <summary>
    /// when the next button is pressed, advance based on current state
    /// </summary>
    public void Next()
    {
        switch(dialogState)
        {
            case 0:
                dialogText.text = nextMessage;
                updateEnemyHealth();
                break;
            case 1:
                if (BattleManager.Instance.isBattleOver)
                {
                    dialogText.text = nextMessage;
                    dialogState = 3;
                }
                else
                {
                    Attack attack = enemy.GetRandomAttack();
                    dialogText.text = enemy.name + " used " + attack.name;
                    useAttack(attack, enemy, player);
                }
                break;
            case 2:
                dialogText.text = nextMessage;
                updatePlayerHealth();

                break;
            case 3:
                if(BattleManager.Instance.isBattleOver)
                {
                    Debug.Log("You ran");
                    dialogText.text = nextMessage;
                }
                else
                {
                    dialogState = 0;
                    DialogMenu.SetActive(false);
                    BattleMenu.SetActive(true);
                }
                return;
            case 4:
                Debug.Log("You ran");
                BattleManager.Instance.checkBattleCondition();
                break;
        }
        dialogState++;
    }

    /// <summary>
    /// Changes the players health bar to represent their health
    /// </summary>
    public void updatePlayerHealth()
    {
        if (player.getHealth() > 0)
        {
            float percent = player.getHealth() / player.getMaxHealth();
            GameObject healthBar = GameObject.Find("PlayerHealth");
            healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 4 * percent);
            setHealthBarColor(healthBar, percent);
        } else
        {
            Debug.Log("Your health is " + player.getHealth());
            BattleManager.Instance.isBattleOver = true;
            BattleManager.Instance.isGameOver = true;
            BattleManager.Instance.enemySurvived = true;
            nextMessage = "You were defeated by " + enemy.name;
            //BattleManager.Instance.checkBattleCondition();
        }


    }

    /// <summary>
    /// changes the enemys health bar to represent their health
    /// </summary>
    public void updateEnemyHealth()
    {
        if (enemy.getHealth() > 0)
        {
            float percent = enemy.getHealth() / enemy.getMaxHealth();
            GameObject healthBar = GameObject.Find("EnemyHealth");
            healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 4 * percent);
            setHealthBarColor(healthBar, percent);
        }
        else
        {
            Debug.Log("Enemy health is " + enemy.getHealth());
            BattleManager.Instance.isBattleOver = true;
            BattleManager.Instance.isGameOver = false;
            BattleManager.Instance.enemySurvived = false;
            nextMessage = "You defeated " + enemy.name;
            //BattleManager.Instance.checkBattleCondition();
        }

    }

    /// <summary>
    /// Sets the colour of the healthbar based on the remaining health
    /// Sets the colour of the healthbar based on the remaining health
    /// </summary>
    /// <param name="healthbar"></param>
    /// <param name="percent"></param>
    public void setHealthBarColor(GameObject healthbar, float percent)
    {

        if (percent > 0.66f)
        {
            healthbar.GetComponent<Image>().color = Color.green;
        }
        else if (percent > 0.33f)
        {
            healthbar.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            healthbar.GetComponent<Image>().color = Color.red;
        }
    }
}
