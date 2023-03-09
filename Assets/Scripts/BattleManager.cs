using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Singleton pattern
    public static BattleManager Instance { get; internal set; }
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Singleton pattern ends

    // Update is called once per frame
    public void HandleUpdate()
    {
        Debug.Log("GameState is currently on battle mode");
    }
}