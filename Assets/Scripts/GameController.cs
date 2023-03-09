using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    Vector3 overworldPosition;

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
                    playerController.HandleUpdate();
                }
                break;
            case GameState.Dialog:
                DialogManager.Instance.HandleUpdate();
                break;
            case GameState.Battle:
                BattleManager.Instance.HandleUpdate();
                break;
            default:
                Debug.LogError("Invalid game state");
                break;
        }

    }

    // Switch to a battle state and starts the battle from BattleManager.
    public void StartBattle(GameObject enemy)
    {
        state = GameState.Battle;
        StartCoroutine(BattleManager.Instance.StartBattle(enemy));
    }
}
