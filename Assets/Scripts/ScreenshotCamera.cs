using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TakeScreenShot());
        }
    }

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
