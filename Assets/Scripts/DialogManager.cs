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
        Instance ??= this; // ??= operator sets Instance to 'this' only if it is currently null, so it's set only once.
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;


    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true); // Changes the visibility of the dialog 
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines(); // Stop the current typing process
                isTyping = false;
                dialogText.text = dialog.Lines[currentLine]; // Show entire dialog text
            }

            else  // Start dialog
            {
                ++currentLine; // Starting line 0, adding one
                if (currentLine < dialog.Lines.Count) // Checks if current line is less than the amount of dialog lines the npc has
                {
                    StartCoroutine(TypeDialog(dialog.Lines[currentLine])); // Shows dialog 
                }
                else
                { // Once there is no more dialog boxes, hide the dialog box from the screen
                    dialogBox.SetActive(false);
                    currentLine = 0;
                    OnHideDialog?.Invoke(); // enables FreeRoam by changing gamestate
                }
            }

        }

    }

    // Shows dialog text letter by letter rather than all at once.
    // Shows all the text at once if the user presses spacebar during the iteration.
    public IEnumerator TypeDialog(string line)
    {

        if (string.IsNullOrEmpty(line))
        {
            yield break; // Exit the coroutine if line is null or empty.
        }

        isTyping = true;
        dialogText.text = ""; // Empty the dialog sentence
        bool skipDialog = false;
        for (int i = 0; i < line.Length; i++)
        {
            if (skipDialog)
            {
                dialogText.text += line.Substring(i);
                break;
            }

            dialogText.text += line[i];

            if (Input.GetKeyDown(KeyCode.Space)) // Waits if the user presses spacebar during the iteration.
            {
                skipDialog = true;
            }

            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }


}
