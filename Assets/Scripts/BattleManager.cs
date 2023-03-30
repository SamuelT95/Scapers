using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class deals with starting and stopping the battle sequnce
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private Vector3 overworldPosition;
    private Vector3 enemyOverworldPosition;
    public bool isBattleOver = false;
    public bool isGameOver = false;
    public bool enemySurvived = true;
    public float coolDownTime = 1.50f;
    public bool coolDown = false;

    // Singleton pattern
    public static BattleManager Instance { get; internal set; }

    /// <summary>
    /// called when activated
    /// </summary>
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Singleton pattern ends

    public void HandleUpdate()
    {
        
    }

    /// <summary>
    /// Checks if a win or lose condition is found in a battle.
    /// </summary>
    public void checkBattleCondition()
    {
        if (isBattleOver)
        {
            isBattleOver = false; // flipping it back to prepare for the next fight
            if (isGameOver)
            {
                Debug.Log("You lose"); // replace after with lose screen
            }
            else
            {
                Debug.Log("You win!"); // replace after with win screen
            }

            EndBattle();
        }
    }

    /// <summary>
    /// Starts a battle with the specified enemy
    /// </summary>
    /// <param name="enemy">The enemy that will be moved to the battle scene</param>
    /// <returns></returns>
    public IEnumerator StartBattle(GameObject enemy)
    {


            Debug.Log("Enemy:" + enemy);
            Scene level = SceneManager.GetActiveScene(); // Gets the current scene (Overworld)
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // Finds the player object
            GameObject eventController = GameObject.Find("EventSystem");

            // loads the battle scene and waits 2 frames, scenes dont load until the end of the frame
            SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            // loading the screenshot from the enemy trigger
            string backgroundScreenshot = PlayerPrefs.GetString("screenshot", "");
            if (backgroundScreenshot != null && backgroundScreenshot != "")
            {
                byte[] bytes = Convert.FromBase64String(backgroundScreenshot);
                Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height);
                screenshotTexture.LoadImage(bytes);

                // set the scene background with the acquired screenshot
                GameObject canvasGameObject = GameObject.Find("Canvas");
                Canvas canvas = canvasGameObject.GetComponent<Canvas>();

                // create a sprite from the screenshot
                canvas.GetComponentInChildren<Image>().sprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), new Vector2(0.5f, 0.5f));

            }

            // store the players overworld position
            overworldPosition = player.transform.position;
            enemyOverworldPosition = enemy.transform.position;
            // stops player from moving freely during the battle phase
            player.GetComponent<PlayerController>().isMoving = false;

            // positions the images of each combatant in the battlescene
            player.transform.position = GameObject.Find("PlayerLocation").transform.position;
            enemy.transform.position = GameObject.Find("EnemyLocation").transform.position;

            // set the battlescenes level indicators to the player/enemies levels
            GameObject playerLevel = GameObject.Find("PlayerLevel");
            playerLevel.transform.GetComponent<TMP_Text>().text = player.GetComponent<Character>().level.ToString();
            player.transform.localScale = new Vector3(2, 2, 2);

            GameObject enemyLevel = GameObject.Find("EnemyLevel");
            enemyLevel.transform.GetComponent<TMP_Text>().text = enemy.GetComponent<Character>().level.ToString();
            enemy.transform.localScale = new Vector3(3, 3, 3);

            // move player and enemy to battle scene, then hide overworld
            Scene battle = SceneManager.GetSceneByName("BattleScene");
            SceneManager.MoveGameObjectToScene(enemy, battle);
            SceneManager.MoveGameObjectToScene(eventController, battle);
            SceneManager.MoveGameObjectToScene(player, battle);
            SceneManager.SetActiveScene(battle);

            // turn off the visibility of objects in the overworld
            foreach (GameObject obj in level.GetRootGameObjects())
            {
                if (obj.GetType() != typeof(EventSystem))
                {
                    obj.SetActive(false);
                }

            }
        
    }

    /// <summary>
    /// unloads the scene after a delay
    /// </summary>
    public void EndBattle()
    {
        
        Debug.Log("Ending battle");
        if (isGameOver)
        {
            Debug.Log("Ending game because you died");
            Application.Quit();
        }
        StartCoroutine(UnloadSceneAfterDelay("FreeRoamWorld", 0.1f));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName">The scene to return to</param>
    /// <param name="delay"> unloads the scene after the specified delay</param>
    /// <returns></returns>
    IEnumerator UnloadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        // get scenes
        Scene battle = SceneManager.GetSceneByName("BattleScene");
        Scene overworldLevel = SceneManager.GetSceneByName(sceneName);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject eventController = GameObject.Find("EventSystem");
        // return player to normal size
        player.transform.localScale = new Vector3(1, 1, 1);

        if(enemySurvived)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            SceneManager.MoveGameObjectToScene(enemy, overworldLevel);
            enemy.transform.position = enemyOverworldPosition;
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }

        // move player back to overworld and unhide overworld
        SceneManager.MoveGameObjectToScene(player, overworldLevel);
        SceneManager.MoveGameObjectToScene(eventController, overworldLevel);
        SceneManager.SetActiveScene(overworldLevel);
        SceneManager.UnloadSceneAsync(battle);

        foreach (GameObject obj in overworldLevel.GetRootGameObjects())
        {
            obj.SetActive(true);
        }

        //return player to overworld position
        player.transform.position = overworldPosition;

        GameController.Instance.ChangeGameState(GameState.FreeRoam);
        coolDown = true;

        yield return new WaitForSeconds(coolDownTime);

        coolDown = false;
    }
    }