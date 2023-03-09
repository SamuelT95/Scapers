using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        Debug.Log("GameState is currently on battle mode");
    }
}