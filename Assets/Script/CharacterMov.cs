using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMov : MonoBehaviour
{
    [SerializeField]
    float speed = 4;
    bool isGrounded;
    bool isStairs;
    int jumpers;
    int slide;

    Rigidbody2D rigi;

    public Animator Anim; 


    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        isGrounded = false;
        jumpers = 0;
        isStairs = false;
        slide = 0;
    }

  
    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Platform")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D Stairs)
    {
        if(Stairs.gameObject.tag== "Stairs")
        {
            isStairs = true;
        }
    }
    private void OnTriggerStay2D(Collider2D Stairs)
    {
        if (Stairs.gameObject.tag == "Stairs")
        {
            isStairs = true;
        }
    }
    private void OnTriggerExit2D(Collider2D Stairs)
    {
        if (Stairs.gameObject.tag == "Stairs")
        {
            isStairs = false;
        }
    }

    void Movement()
    {

        // Movement
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            Anim.SetTrigger("ForRun");
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            Anim.SetTrigger("ForRun");
        }


        //Climb
        if (isStairs)
        {
          if (Input.GetKey(KeyCode.W))
          {
            rigi.velocity = new Vector2(0, speed);
            Anim.SetTrigger("ForClimb");
          } 
          if (Input.GetKey(KeyCode.S))
          {
            rigi.velocity = new Vector2(0, -speed);
            Anim.SetTrigger("ForClimb");
          }
        }
        



        // Ground Control
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded == true || jumpers < 2)
            {
                Jump();
                isGrounded = false;
                jumpers++;
            }

           
        }
       
        if (isGrounded == true)
        {
            jumpers = 0;
        }

        //Defense

        if (Input.GetKey(KeyCode.E))
        {
            if (slide == 0)
            {
                slide = 1;
                transform.Translate(2 * Time.deltaTime, 0, 0);
                Anim.SetTrigger("ForSlide");
            }
            slide = 0;
            
        }




        // Attacks1
        if (Input.GetKey(KeyCode.Return))
        {
            Anim.SetTrigger("ForAttacks");
        }
    }


    // Jump
    void Jump()
    {
        rigi.velocity = new Vector2(0, 2);

        rigi.velocity += new Vector2(0, 2);
    }

}




