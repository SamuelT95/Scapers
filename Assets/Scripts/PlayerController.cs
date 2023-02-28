using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // determines how fast the player can move
    private bool isMoving; // checks player if moving
    private Vector2 input; // Vector2 class (specific to unity) holds x and y coords
    private Animator animator;
    public LayerMask solidObjectsLayer;


    // Loads the character and animates it
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Update() // 60 times per second this function will run
    {
        if (!isMoving) // If character is not moving, then keep checking for input
        {
            // Stores movement keys if they are pressed
            input.x = Input.GetAxisRaw("Horizontal"); // checks left and right arrow keys
            input.y = Input.GetAxisRaw("Vertical"); // checks down and up arrow 

            if (input.x != 0) input.y = 0; // Disables diagonal movement

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
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
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
        if (Physics2D.OverlapCircle(targetPosition, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
