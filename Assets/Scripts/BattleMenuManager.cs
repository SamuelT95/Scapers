using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the menus with a batlle scene
/// </summary>
public class BattleMenuManager : MonoBehaviour
{
    private enum dialogMenuState
    {
        PLAYER_USED,
        PLAYER_DAMAGE,
        ENEMY_USED,
        ENEMY_DAMAGE,
        EXP,
        RUN,
        BATTLE_RESULT,
        END,
        BATTLE_OVER
    }
    //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");*/

    private GameObject BattleMenu;
    private GameObject AttackMenu;
    private GameObject PhysicalAttackMenu;
    private GameObject MagicAttackMenu;
    private GameObject DialogMenu;

    private string nextMessage;
    private dialogMenuState dialogState = dialogMenuState.PLAYER_USED;

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

        Debug.Log("healthupdated");
        StartCoroutine(resetHealthBars());

    }

    IEnumerator resetHealthBars()
    {
        yield return new WaitForEndOfFrame();
        updateEnemyHealth();
        updatePlayerHealth();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();
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
        Heal heal = ScriptableObject.CreateInstance<Heal>();
        heal.Damage = 3;
        heal.Name = "Healing Power";
        heal.FailureRate = 0.1f;

        BattleMenu.SetActive(false);
        DialogMenu.SetActive(true);
        useAttack(heal, player, player);
    }

    public void Run()
    {
        BattleMenu.SetActive(false);
        DialogMenu.SetActive(true);
        dialogState = dialogMenuState.RUN;
        Next();
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
        dialogState = dialogMenuState.PLAYER_USED;
        Next();
    }

    /// <summary>
    /// called when the player uses an attack
    /// </summary>
    /// <param name="buttonNo">The number of the button that was pressed</param>
    public void PhysAttack(int buttonNo)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();
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
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();
        MagicAttackMenu.SetActive(false);
        DialogMenu.SetActive(true);
        useAttack(player.magicalAttacks[buttonNo], player, enemy);
    }

    /// <summary>
    /// when the next button is pressed, advance based on current state
    /// </summary>
    public void Next()
    {
        Debug.Log(dialogState);
        switch (dialogState)
        {
            case dialogMenuState.PLAYER_USED:
                dialogState = dialogMenuState.PLAYER_DAMAGE;
                break;
            case dialogMenuState.PLAYER_DAMAGE:
                dialogText.text = nextMessage;
                updateEnemyHealth();
                updatePlayerHealth();
                if (enemy.getHealth() <= 0)
                {
                    BattleManager.Instance.isBattleOver = true;
                    BattleManager.Instance.isGameOver = false;
                    BattleManager.Instance.enemySurvived = false;
                    dialogState = dialogMenuState.BATTLE_RESULT;
                }
                else
                {
                    dialogState = dialogMenuState.ENEMY_USED;
                }
                break;
            case dialogMenuState.ENEMY_USED:
                Attack attack = enemy.GetRandomAttack();
                dialogText.text = enemy.name + " used " + attack.name;
                useAttack(attack, enemy, player);
                dialogState = dialogMenuState.ENEMY_DAMAGE;
                break;
            case dialogMenuState.ENEMY_DAMAGE:
                dialogText.text = nextMessage;
                updateEnemyHealth();
                updatePlayerHealth();
                if (player.getHealth() <= 0)
                {
                    BattleManager.Instance.isBattleOver = true;
                    BattleManager.Instance.isGameOver = true;
                    BattleManager.Instance.enemySurvived = true;
                    dialogState = dialogMenuState.BATTLE_RESULT;
                }
                else
                {
                    dialogState = dialogMenuState.END;
                }
                break;
            case dialogMenuState.BATTLE_RESULT:
                if (BattleManager.Instance.enemySurvived == false)
                {
                    dialogText.text = player.name + " defeated " + enemy.name + "!";
                    dialogState = dialogMenuState.EXP;
                }
                else
                {
                    dialogText.text = player.name + " was defeated by " + enemy.name + "!" + "\n You blacked out";
                    dialogState = dialogMenuState.BATTLE_OVER;
                }
                break;
            case dialogMenuState.EXP:
                int level = player.level;
                int expGained = player.AddExp(enemy.level);

                string message = player.name + "gained " + expGained.ToString() + " exp";

                if (level < player.level)
                {
                    message += "\n You leveled up! You are now level: " + player.level.ToString();
                    
                }

                dialogText.text = message;
                dialogState = dialogMenuState.BATTLE_OVER;
                break;

            case dialogMenuState.END:
                //end dialog
                DialogMenu.SetActive(false);
                BattleMenu.SetActive(true);
                break;

            case dialogMenuState.BATTLE_OVER:
                player.Heal(1.0f);
                BattleManager.Instance.checkBattleCondition();
                break;

            case dialogMenuState.RUN:
                int rand = UnityEngine.Random.Range(0, 4);
                if (rand == 0)
                {
                    dialogText.text = "You failed to run away!";
                    dialogState = dialogMenuState.ENEMY_USED;
                }
                else
                {
                    dialogText.text = "You ran away!";
                    dialogState = dialogMenuState.BATTLE_OVER;

                    BattleManager.Instance.isBattleOver = true;
                    BattleManager.Instance.isGameOver = false;
                    BattleManager.Instance.enemySurvived = true;
                    Debug.Log("You ran");
                }
                break;
            
        }
    }

    /// <summary>
    /// Changes the players health bar to represent their health
    /// </summary>
    public void updatePlayerHealth()
    {
        GameObject healthBar = GameObject.Find("PlayerHealth");
        if (player.getHealth() > 0)
        {
            float percent = player.getHealth() / player.getMaxHealth();
            healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 4 * percent);
            setHealthBarColor(healthBar, percent);
        } else
        {
            healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            Debug.Log("Your health is " + player.getHealth());

            nextMessage = "You were defeated by " + enemy.name;
            //BattleManager.Instance.checkBattleCondition();
        }


    }

    /// <summary>
    /// changes the enemys health bar to represent their health
    /// </summary>
    public void updateEnemyHealth()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();
        GameObject healthBar = GameObject.Find("EnemyHealth");
        if (enemy.getHealth() > 0)
        {
            float percent = enemy.getHealth() / enemy.getMaxHealth();
            
            healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 4 * percent);
            setHealthBarColor(healthBar, percent);
        }
        else
        {
            healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
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
