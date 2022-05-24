using System.Collections;
using UnityEngine;

public class Wizard: MonoBehaviour
{
    float WizardspeedX = 4f;
    float WizardspeedY = 2f;
    public Animator wizardAn;

    public GameObject[] sentences = new GameObject[6];
    GameObject text;
    GameObject text1;
    int counter;
    public GameObject dialoguePanel;

    bool fly;
    private void Start()
    {
        fly = true;
        counter = 1;
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
    public void Next()
    {
        if (counter == 5)
        {
            dialoguePanel.SetActive(false);
            wizardAn.SetBool("Die", true);
            StartCoroutine(Deletewizard());
        }
        text = sentences[counter - 1];
        text1 = sentences[counter];
        text.gameObject.SetActive(false);
        text1.gameObject.SetActive(true);
        counter++;
        
        
    }
    IEnumerator Deletewizard()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
