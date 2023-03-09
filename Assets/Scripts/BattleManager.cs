using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
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

    private void Start()
    {
        
    }

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

        // loads the battle scene and waits 2 frames, scenes dont load until the end of the frame
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

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
        SceneManager.MoveGameObjectToScene(player, battle);
        SceneManager.SetActiveScene(battle);

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

        //get scenes
        Scene battle = SceneManager.GetSceneByName("BattleScene");
        Scene level = SceneManager.GetSceneByName("FreeRoamWorld");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //return player to normal size
        player.transform.localScale = new Vector3(1, 1, 1);

        //move player back to overworld and unhide overworld
        SceneManager.MoveGameObjectToScene(player, level);
        SceneManager.SetActiveScene(level);
        SceneManager.UnloadSceneAsync(battle);

        foreach (GameObject obj in level.GetRootGameObjects())
        {
            obj.SetActive(true);
        }

        //return player to oveworld position
        player.transform.position = overworldPosition;

        GameController.Instance.ChangeGameState(GameState.FreeRoam);
    }
}