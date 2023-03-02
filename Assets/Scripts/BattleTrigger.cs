using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("You collided with this object");
            Destroy(gameObject);
            GameController.Instance.ChangeGameState(GameState.Battle);
            UnityEngine.SceneManagement.SceneManager.LoadScene("anotherscene");

        }
    }
}
