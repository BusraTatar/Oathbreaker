using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dialog;
    public string[] sentences = new string[5];
    void Start()
    {
        string [] sentences= { "I guess I don't know you. Who are you too?", "This is a very wrong question. What matters is not who I am. I've come to warn you against prophecy." };
    }

   
    void Update()
    {
        
    }

    public void Next()
    {
        for (int i = 0; i <5; i++)
        {   
            string text = sentences[i];
            dialog.text = text.ToString();
        }
    }


}
