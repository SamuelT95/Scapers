using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;

    void Update()
    {


        //Debug.Log("MenuController pushing updates");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Menu opened");
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
