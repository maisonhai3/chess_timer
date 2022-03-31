using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoRedOnClick : MonoBehaviour
{
    [SerializeField] Image goRedImage;
    [SerializeField] bool defaultIsGreen = true;
    
    private void OnEnable()
    {
        // Set default color to Green.
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        goRedImage.color = Color.red;
        Debug.Log("DONE");
        
    }
}
