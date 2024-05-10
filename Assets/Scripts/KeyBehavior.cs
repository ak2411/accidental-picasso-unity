using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBehavior : MonoBehaviour
{
    public string letter;
    private KeyboardManager keyboardManager;

    private void Awake()
    {
        keyboardManager = FindObjectOfType<KeyboardManager>();
    }

    public void TypeKey()
    {
        if(string.IsNullOrEmpty(letter))
        {
            letter = GetComponentInChildren<TMP_Text>().text ?? "";
        }
        keyboardManager.AddLetter(letter);
    }
}
