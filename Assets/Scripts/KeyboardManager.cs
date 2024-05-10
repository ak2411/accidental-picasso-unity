using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject key_prefab;

    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private Vector3 create_keys_start = new Vector3(-0.1452f, 0.023f, 0.0567f);

    [SerializeField]
    private Vector3 gap = new Vector3(0.055f, 0.0f, -0.05f);

    private List<string> rows = new List<string> { "qwertyuiop", "asdfghjkl", "zxvcbnm" };

    public string Text => inputField ? inputField.text : string.Empty;

    void Awake()
    {
        CreateKeys();
    }

    public void OnBackspace()
    {
        if (!inputField || string.IsNullOrEmpty(inputField.text))
        {
            return;
        }
        inputField.text = Text.Substring(0, Text.Length - 1);
    }

    public void OnEnter()
    {
        gameObject.SetActive(false);
    }

    public void CreateKeys()
    {
        Vector3 curr_key_pos = create_keys_start;
        for(int i = 1; i < 11; i ++)
        {
            CreateKey((i%10).ToString(), curr_key_pos);
            curr_key_pos += Vector3.right * gap.x;
        }
        curr_key_pos = create_keys_start + Vector3.forward * gap.z;
        for(int i=0; i < rows.Count; i++)
        {
            foreach (char c in rows[i])
            {
                CreateKey(c.ToString(), curr_key_pos);
                curr_key_pos += Vector3.right * gap.x;
            }
            curr_key_pos = create_keys_start + Vector3.forward * gap.z * (i+2) + Vector3.right * gap.x * i *0.5f;
        }
    }

    private void CreateKey(string value, Vector3 localPos)
    {
        var newKey = Instantiate(key_prefab);
        newKey.GetComponentInChildren<TMP_Text>().text = value;
        newKey.transform.SetParent(transform, false);
        newKey.transform.localPosition = localPos;
    }

    public void AddLetter(string letter)
    {
        if(!inputField)
        {
            return;
        }
        inputField.text += letter;
    }
}
