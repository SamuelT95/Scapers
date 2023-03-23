using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private Vector3 overworldPosition;
    // Singleton pattern
    public static BattleManager Instance { get; internal set; }
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

    // Update is called once per frame. This is where you detect keystrokes
    public void HandleUpdate()
    {
        /*Debug.Log("GameState is currently on battle mode");*/

    }

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
            obj.SetActive(false);
        }
        EndBattle();
    }


    public void EndBattle()
    {
        StartCoroutine(UnloadSceneAfterDelay("FreeRoamWorld", 5f)); // 5 second delay
        Debug.Log("Ending battle");

    }

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
    }
}