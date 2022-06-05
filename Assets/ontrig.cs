using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ontrig : MonoBehaviour
{
    public GameObject[] skeleton = new GameObject[5];
    int counter;
    int i;
    GameObject obje;
    
    void Start()
    {
        counter = 0;
        i = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            counter += 1;
            if(counter == 5)
            {
               
                obje= skeleton[i];
                counter = 0;
                Destroy(obje);
                i++;
            }
            
        }
    }
    
}
