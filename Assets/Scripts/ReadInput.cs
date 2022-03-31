using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReadInput : MonoBehaviour
{
    [SerializeField] 
    TMP_InputField nameInputField;
    
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(nameInputField.text);
        }
    }
}
