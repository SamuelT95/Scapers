using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed; // determines how fast the player can move

    private void Update() // 60 times per second this function will run
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

}
