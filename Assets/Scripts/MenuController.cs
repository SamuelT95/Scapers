using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject quitButton;
    public GameObject continueButton;

    void Update()
    {
        //Debug.Log("MenuController pushing updates");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Menu opened");
            menuPanel.SetActive(!menuPanel.activeSelf);
            quitButton.SetActive(!quitButton.activeSelf);
            continueButton.SetActive(!continueButton.activeSelf);

        }
    }
}
