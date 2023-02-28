using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class allows us to add text to dialogue boxes within the Unity inspector. 
/// </summary>
public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    // Shows the DialogManager to a global scope
    public static DialogManager Instance { get; private set; } 
    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;


    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true); // Changes the visibility of the dialog box
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !isTyping) // Start dialog, and disables button mashing to cause disorder.
        {
            ++currentLine; // Starting line 0, adding one
            if (currentLine < dialog.Lines.Count) // Checks if current line is less than the amount of dialog lines the npc has
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine])); // Shows dialog 
            } 
            else
            { // Once there is no more dialog boxes, hide the dialog box from the screen
                dialogBox.SetActive(false); 
                OnHideDialog?.Invoke(); // enables FreeRoam by changing gamestate
            }
        }
    }

    // Shows dialog text letter by letter rather than all at once.
    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = ""; // Empty the dialog sentence
        foreach (var letter in line.ToCharArray()) // Adds 1 letter at a time
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
        isTyping = false;
    }

    
}
