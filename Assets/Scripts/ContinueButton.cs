using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public MenuController menuController;
    public void ContinueGame()
    {
        Debug.Log("Continue Game!");
        menuController.menuPanel.SetActive(!menuController.menuPanel.activeSelf);
        menuController.quitButton.SetActive(!menuController.quitButton.activeSelf);
        menuController.continueButton.SetActive(!menuController.continueButton.activeSelf);
    }
}
