using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        Debug.Log("You are interacting with a merchant");
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
