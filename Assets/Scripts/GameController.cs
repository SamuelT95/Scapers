using System.Collections;
using System.Collections.Generic;
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

    GameState state;

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

        if (state == GameState.Battle)
        {
            EndBattle();
        }
    }

    public void EndBattle()
    {
        StartCoroutine(UnloadSceneAfterDelay("FreeRoamWorld", 1f)); // 1 second delay
        Debug.Log("Ending battle");
        ChangeGameState(GameState.FreeRoam);
    }

    IEnumerator UnloadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("FreeRoamWorld");
    }
}
