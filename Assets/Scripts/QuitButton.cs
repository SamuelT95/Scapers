using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame() // Exits the game when the button is clicked.
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }
}
