using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    // SerializeField exposes the PlayerController field in the Unity inspector.
    [SerializeField] PlayerController playerController;

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

    GameState state;
    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        } else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        } else if (state == GameState.Battle)
        {

        }
    }
}