using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class CaveLevel : MonoBehaviour
{
    public GameObject loadingPanel;

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            loadingPanel.SetActive(true);
            StartCoroutine(Level());
        }
    }

    IEnumerator Level()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

 

