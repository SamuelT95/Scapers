using TMPro;
using UnityEngine;

/// <summary>
/// responsible for the movement off ghost enemies
/// </summary>
public class GhostController : MonoBehaviour
{
    public float moveSpeed; // determines how fast the player can move
    private float currentSpeed = 0;
    public float acceleration;
    public float engagementDistance;

    /// <summary>
    /// 60 times per second this function will run, tells the ghost to move to the player if within distance and player is not facing ghost
    /// </summary>
    private void Update()
    {
        //prevents movement when in battle state
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameController.state == GameState.Battle)
        {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector3.Distance(player.transform.position, transform.position);

        //if player is in range
        if (distance < engagementDistance)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            float xDir = pc.animator.GetFloat("moveX");
            float yDir = pc.animator.GetFloat("moveY");

            //if the player is looking at ghost, do nothing
            if((xDir < 0 && player.transform.position.x - transform.position.x > 0) || (xDir > 0 && player.transform.position.x - transform.position.x < 0) || (yDir > 0 && player.transform.position.y - transform.position.y < 0) || (yDir < 0 && player.transform.position.y - transform.position.y > 0))
            {
                return;
            }

            if (currentSpeed < moveSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        
    }

}
