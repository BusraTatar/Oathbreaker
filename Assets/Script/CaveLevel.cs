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
          
        }
    }

  
}

 

