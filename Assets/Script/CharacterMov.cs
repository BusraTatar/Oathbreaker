using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMov : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0f;

    private bool isGrounded;

    private bool isStairs;

    private int jumpers;

    private int slide;

    private Animator _animator;

    private Rigidbody2D r2d;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>(); //caching Rigidbody2D
        _spriteRenderer = GetComponent<SpriteRenderer>(); //caching SpriteRenderer
        _animator = GetComponent<Animator>(); //caching Animator
        isGrounded = true;
        isStairs = false;
        slide = 0;
    }

    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _animator.SetBool("grounded", true);
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D Stairs)
    {
        if (Stairs.gameObject.tag == "Stairs")
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
            _spriteRenderer.flipX = false;
            speed = 4.0f;
            transform.Translate(speed * Time.deltaTime, 0, 0);
            _animator.SetFloat("speed", speed);
            _animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            speed = 4.0f;
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            _animator.SetFloat("speed", speed);
            _animator.SetBool("moving", true);
        }
        else
        {
            speed = 0;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("moving", false);
        }

        //Climb
        if (isStairs)
        {
            if (Input.GetKey(KeyCode.W))
            {
                r2d.velocity = new Vector2(0, speed);
                _animator.SetTrigger("ForClimb");
            }
            if (Input.GetKey(KeyCode.S))
            {
                r2d.velocity = new Vector2(0, -speed);
                _animator.SetTrigger("ForClimb");
            }
        }

        // Ground Control
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                Jump();
                isGrounded = false;
                _animator.SetTrigger("jump");
                _animator.SetBool("grounded", false);
            }
        }

        //Defense
        if (Input.GetKey(KeyCode.E))
        {
            if (slide == 0)
            {
                slide = 1;
                transform.Translate(2 * Time.deltaTime, 0, 0);
                _animator.SetTrigger("ForSlide");
            }
            slide = 0;
        }

        // Attacks1
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("ForAttacks");
        }
    }

    // Jump
    void Jump()
    {
        r2d.velocity = new Vector2(r2d.velocity.x, 2);

        r2d.velocity += new Vector2(0, 2);
    }
}
