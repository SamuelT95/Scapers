using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// responsible for creating a screenshot of the environment and using it as the battle scene background
/// </summary>
public class ScreenshotCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Camera screenshotCamera;
    [SerializeField] string ignoreTag;

    /// <summary>
    /// when the condition is met take the screenshot
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TakeScreenShot());
        }
    }

    /// <summary>
    /// Takes a picture and sets it to a canvas
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        // Take Screenshot
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotCamera.Render();
        RenderTexture.active = screenshotCamera.targetTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();
        Debug.Log("Screenshotted!");

        // Transfer screenshot to next scene
        PlayerPrefs.SetString("screenshot", Convert.ToBase64String(screenshot.EncodeToPNG()));
    }
}   
