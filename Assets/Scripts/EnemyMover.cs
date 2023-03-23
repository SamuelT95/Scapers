using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float moveSpeed; // determines how fast the player can move
    public bool isMoving; // checks player if moving
    private Vector2 input; // Vector2 class (specific to unity) holds x and y coords
    public Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public float engagementDistance;

    // Loads the character and animates it
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Update() // 60 times per second this function will run
    {
        //prevents movement when in battle state
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameController.state == GameState.Battle)
        {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector3.Distance(player.transform.position, transform.position);



        if (!isMoving) // If character is not moving, then keep checking for input
        {
            //if player is in range
            if (distance < engagementDistance)
            {
                input.x = 0.0f;
                input.y = 0.0f;

                if (Mathf.Abs(player.transform.position.x - transform.position.x) >= Mathf.Abs(player.transform.position.y - transform.position.y))
                {
                    if (player.transform.position.x < transform.position.x)
                    {
                        input.x = -1.0f;
                    }
                    else
                    {
                        input.x = 1.0f;
                    }
                }
                else
                {
                    if (player.transform.position.y < transform.position.y)
                    {
                        input.y = -1.0f;
                    }
                    else
                    {
                        input.y = 1.0f;
                    }
                }
            }

            if (input != Vector2.zero) // If user types any arrow keys
            {
                // Set animation to the direction of user input
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                // Transform is Unity property of an object. Transform.position holds xyz coordinates.
                var targetPosition = transform.position; // Current position of character in the world
                // add new positions in the world into temporary variable
                targetPosition.x += input.x; // input.x is 1 or -1
                targetPosition.y += input.y; // input.y is 1 or -1

                // Checks if the tile is walkable first.
                if (IsWalkable(targetPosition)) StartCoroutine(Move(targetPosition)); // Move position to target position
            }
        }

        animator.SetBool("isMoving", isMoving);

        // Interaction command is spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    // Checks if the position in front of the player has an interactable object.
    void Interact()
    {
        var facingDirection = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDirection; // This is the target interaction area in front of player.
        // Debug.DrawLine(transform.position, interactPosition, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPosition, 0.2f, interactableLayer); // Checks if the target area has an interactable object.
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact(); // If the target is interactable, then run func Interact();
        }

    }

    // This function will move current position to new desired position using movement speed.
    // It will slide the player's character to the new position, instead of teleporting.
    // Vector3 has an xyz value and Move requires Vector3.
    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;

        // Basically, this while loop detects movement.
        // Takes the new position subtracted by original position.
        // If there a value, then this while loop will execute.
        // Mathf.Epsilon is an extremely small number
        // sqrMagnitude square roots the vector.
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon && GameController.Instance.state != GameState.Battle)
        {
            // Synchronizes the user's frame rate and the game.
            // Time.deltaTime are intervals in seconds from last frame to current frame
            // transform.position is current player position in the world.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }

    // Checks if a tile in the overworld is a walkable tile.
    private bool IsWalkable(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition, 0.001f, solidObjectsLayer | interactableLayer) != null)
        {
            return false; // Blocks the player from walking into this tile.
        }
        else
        {
            return true; // Allows the player to walk on this tile.
        }
    }
}
