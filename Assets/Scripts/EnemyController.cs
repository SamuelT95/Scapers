using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        Debug.Log("You will start a battle!");
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }

}
