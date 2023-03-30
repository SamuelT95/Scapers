using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Battle triggers:
/// 1. Randomly walking around on specific tiles.
/// 2. Talking to an NPC
/// 3. A npc chases you and collides with your player.
/// 
/// Display Battle Scene:
/// 1. Player figurine
/// 2. Enemies
/// 3. HP count
/// 4. Level
/// 5. Options (fight moves/run)
/// 
/// Attack
/// 1. Use fight moves
/// 2. Animate
/// 
/// Enemy Attacks
/// 1. Use fight moves
/// 2. Animate
/// 
/// When someone dies, give xp / level up / game over.
/// 
/// Exit to Overworld.
/// </summary>
/// 

public class BattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameController.Instance.state != GameState.Battle && BattleManager.Instance.coolDown == false)
        {
            Debug.Log("You collided with " + gameObject);
            Debug.Log("Calling StartBattle function");
            GameController.Instance.StartBattle(gameObject);
            /*GameController.Instance.state = GameState.Battle;*/
        }
    }
}
