using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMov : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0f;

    private float slideSpeed = 2.0f;

    private float vertical;

    private float horizontal;

    private bool isGrounded;

    private bool isSlide;

    private bool isLadder;

    private int jumpers;

    private int slide;

    private Animator _animator;

    private Rigidbody2D r2d;

    private SpriteRenderer _spriteRenderer;

    public BoxCollider2D regularColl;

    public BoxCollider2D slideColl;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>(); //caching Rigidbody2D
        _spriteRenderer = GetComponent<SpriteRenderer>(); //caching SpriteRenderer
        _animator = GetComponent<Animator>(); //caching Animator
        isGrounded = true;
        isSlide = false;
        isLadder = false;
    }

    void Update()
    {
        Movement();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _animator.SetBool("grounded", true);
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
    

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        r2d.velocity = new Vector2(horizontal * speed, r2d.velocity.y);
    }

    void Movement()
    {
        // Movement
        if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            speed = 8.0f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            speed = 8.0f;
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
        if (isLadder)
        {
            r2d.gravityScale = 0;
            if (Input.GetKey(KeyCode.W))
            {
                r2d.velocity = new Vector2(r2d.velocity.x, vertical * 4);
                _animator.SetBool("isClimbing", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                r2d.gravityScale = 0f;
                r2d.velocity = new Vector2(0, vertical * 4);
                _animator.SetBool("isClimbing", true);
            }
            else
            {
                _animator.SetBool("isClimbing", false);
            }
        }
        else
        {
            _animator.SetBool("isClimbing", false);
            r2d.gravityScale = 10.0f;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSlide = true;
            _animator.SetBool("isSlide", true);
            regularColl.enabled = false;
            slideColl.enabled = true;

            if (!_spriteRenderer.flipX)
            {
                r2d.AddForce(Vector2.right * slideSpeed);
            } else
            {
                r2d.AddForce(Vector2.left * slideSpeed);
            }
            StartCoroutine(stopSlide());
        }

        IEnumerator stopSlide(){
            yield return new WaitForSeconds(0.8f);
            if(speed > 0){
                _animator.Play("Run");
                _animator.SetBool("isSlide", false);
                regularColl.enabled = true;
                slideColl.enabled = false;
                isSlide = false;
            }else
            {
                _animator.Play("Idle");
                _animator.SetBool("isSlide", false);
                regularColl.enabled = true;
                slideColl.enabled = false;
                isSlide = false;
            }
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
        r2d.velocity = new Vector2(r2d.velocity.x, 50);

        //r2d.velocity += new Vector2(0, 8);
    }
}
