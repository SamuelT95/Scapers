using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findplayertest : MonoBehaviour
{
    // Start is called before the first frame update
    /*    void Start()
        {

        }
    */
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            Debug.Log("Player position is " + playerPosition);
        }

        
    }
}
