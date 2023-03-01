using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed; // determines how fast the player can move
    private float currentSpeed = 0;
    public float acceleration;
    public float engagementDistance;

    private void Update() // 60 times per second this function will run
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        var distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < engagementDistance)
        {
            if(currentSpeed < moveSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
        }
        
    }

}
