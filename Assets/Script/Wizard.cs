using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard: MonoBehaviour
{
    float WizardspeedX = 4f;
    float WizardspeedY = 2f;
    public Animator wizardAn;

    public GameObject dialoguePanel;

    bool fly;
    private void Start()
    {
        fly = true;
    }


    private void Update()
    {
        if (fly) 
        {
            Flying();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Trigger")
         {
            wizardAn.SetBool("Fly", false);          
            fly = false;
            dialoguePanel.SetActive(true);
        }
       
    }
    
    void Flying()
    {
        transform.Translate(-WizardspeedX * Time.deltaTime, -WizardspeedY * Time.deltaTime, 0);
        wizardAn.SetBool("Fly", true);
    }

}
