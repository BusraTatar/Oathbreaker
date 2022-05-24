using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public Rigidbody2D rb;
    
    void Start()
    {
       
    }

   
    void Update()
    {
        
    }
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.gravityScale = 1;
        }
    }
}
