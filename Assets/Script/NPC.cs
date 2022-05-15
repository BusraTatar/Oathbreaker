using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject PressE;
    public GameObject DialoguePanel;


    private void Update()
    {
        Dialogue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== "Player")
        {
            PressE.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PressE.gameObject.SetActive(false);
        }
    }


    public void Dialogue()
    {
        if(PressE.activeSelf & Input.GetKey(KeyCode.E))
        {
            DialoguePanel.SetActive(true);
            Time.timeScale=0;
        }
    }


   

}
