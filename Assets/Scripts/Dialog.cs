using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Access to the dialog through the inspector
public class Dialog
{
    [SerializeField] List<string> lines; // Stores all of the sentences for a dialog.

    public List<string> Lines
    {
        get { return lines; }
        set { lines = value; }
    }


}
