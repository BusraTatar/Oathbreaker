using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public TextMeshProUGUI dialog;
    public GameObject[] sentences = new GameObject[5];
    GameObject text;
    GameObject text1;
    int counter;
    public GameObject dialogpanel;
    void Start()
    {
        
        counter = 1;
       
    }

   
    void Update()
    {
        
    }

    public void Next()
    {
        
            text = sentences[counter - 1];
            text1 = sentences[counter];
            text.gameObject.SetActive(false);
            text1.gameObject.SetActive(true);
            counter++;
        if (counter >= 5)
        {
            dialogpanel.SetActive(false);
        }
    }


}
