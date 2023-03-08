using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// FreeRoam: Player can freely walk around.
/// Dialog: Player is locked into a dialogue with an object.
/// Battle: Player is locked into a battle scene.
/// </summary>
public enum GameState
{
    FreeRoam, Dialog, Battle
}

    // GameController controls the state of the game, such as menu browsing, combat, dialogue.
public class GameController : MonoBehaviour
{
    Vector3 oldpos;

    public static GameController Instance { get; private set; } // Static instance property, singleton

    // SerializeField exposes the PlayerController field in the Unity inspector.
    [SerializeField] PlayerController playerController;

    private void Awake() // Singleton pattern
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

    private void Start() // A way to switch game states
    {

/*        // Load the initial scene
        SceneManager.LoadScene("FreeRoamWorld");*/


        DialogManager.Instance.OnShowDialog += () => // Lambda expression
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.FreeRoam;
            }
        };
        
    }

    public GameState state;

    public void ChangeGameState(GameState state)
    {
        this.state = state;
    }
    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                if (state == GameState.FreeRoam && playerController != null)
                {
/*                Debug.Log("GameState is currently on FreeRoam mode");
*/
                    playerController.HandleUpdate();
                }
                break;
            case GameState.Dialog:
                DialogManager.Instance.HandleUpdate();
/*                Debug.Log("GameState is currently on dialog mode");
*/
                break;
            case GameState.Battle:
                Debug.Log("GameState is currently on battle mode");
                break;
            default:
                Debug.LogError("Invalid game state");
                break;
        }

    }

    public IEnumerator StartBattle(GameObject enemy)
    {
        state = GameState.Battle;
        Scene level = SceneManager.GetActiveScene();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //laods the battle scene and waits a frame, scenes dont load until the end of the frame
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        //store the players overworld position
        oldpos = player.transform.position;


        player.GetComponent<PlayerController>().isMoving = false;
        player.transform.position = GameObject.Find("PlayerLocation").transform.position;
        enemy.transform.position = GameObject.Find("EnemyLocation").transform.position;

        //set the battlescenes level indicators to the player/enemies levels
        GameObject playerLevel = GameObject.Find("PlayerLevel");
        playerLevel.transform.GetComponent<TMP_Text>().text = player.GetComponent<Character>().level.ToString();
        player.transform.localScale = new Vector3(2,2,2);

        GameObject enemyLevel = GameObject.Find("EnemyLevel");
        enemyLevel.transform.GetComponent<TMP_Text>().text = enemy.GetComponent<Character>().level.ToString();
        enemy.transform.localScale = new Vector3(3,3,3);

        //Move player and enemy to battle scene, then hide overworld
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
        StartCoroutine(UnloadSceneAfterDelay("FreeRoamWorld", 5f)); // 1 second delay
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
        player.transform.position = oldpos;

        ChangeGameState(GameState.FreeRoam);
    }
}
