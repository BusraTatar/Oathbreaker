using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMov : MonoBehaviour
{
    //checkpoint
    [SerializeField] private Vector2[] _checkPoints;
    public static Vector2 _lastTransform;


    public static CharacterMov instance;

    private bool facingRight;

    private float speed = 4.0f;

    private float jumpSpeed = 7.5f;

    private float jumpDelay = 0.5f;

    private float jumpTimer;

    private float fallMultiplier = 5f;

    private float slideSpeed = 2.0f;

    private float gravity = 1f;

    private float linearDrag = 4.0f;

    private float vertical;

    private float horizontal;

    private bool isGrounded;

    private float groundLength = 1.25f;

    private bool isJump;

    private bool isSlide;

    private bool isLadder;

    

    //private int jumpers;

    //private int slide;

    public Vector3 colliderOffset;

    public ParticleSystem dust;

    public Animator _animator;

    private Rigidbody2D r2d;

    public SpriteRenderer _spriteRenderer;

    public bool isAttacking;

    public BoxCollider2D regularColl;

    public BoxCollider2D slideColl;

    public LayerMask groundLayer;



    //healthbar
    public Slider HealthBar;
    //failed
    public GameObject FailPanel;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>(); //caching Rigidbody2D
        _animator = GetComponent<Animator>(); //caching Animator
        facingRight = true;
        isGrounded = false;
        isSlide = false;
        isLadder = false;
        isJump = false;
        //checkpoint
        transform.position = _lastTransform;

        //healthBar
        HealthBar.value = 1f;
        
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        
        Movement();
        Attack();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        if (HealthBar.value == 0)
        {
            FailPanel.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        r2d.velocity = new Vector2(horizontal * speed, r2d.velocity.y);
        
        if (jumpTimer > Time.time && isGrounded)
        {
            Jump();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            _animator.SetBool("grounded", true);
            if (!isJump)
            {
                isGrounded = true;
            }
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FailObject"))
        {
            HealthBar.value -= 0.2f;

        }
        if (collision.gameObject.CompareTag("Failed"))
        {
            HealthBar.value -= 1f;

        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            _animator.SetBool("grounded", true);
            if (!isJump)
            {
                isGrounded = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }

    void Movement()
    {
        // Movement
        if (Input.GetKey(KeyCode.D))
        {
           
            if (!isSlide && !facingRight)
            {
                _spriteRenderer.flipX = false;
                facingRight = true;
                if (isGrounded)
                {
                    CreateDust();
                }
            }
            speed = 8.0f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!isSlide && facingRight)
            {
                _spriteRenderer.flipX = true;
                facingRight = false;
                if (isGrounded)
                {
                    CreateDust();
                }
            }
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
            r2d.drag = 10f;
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

        if (isGrounded && !isLadder)
        {
            r2d.gravityScale = 1;
            isJump = false;
            _animator.SetBool("grounded", true);
            _animator.SetBool("isClimbing", false);
        }
        else if (!isGrounded && !isLadder)
        {
            r2d.gravityScale = gravity;
            r2d.drag = linearDrag * 0.15f;
            if(r2d.velocity.y < 0)
            {
                r2d.gravityScale = gravity * fallMultiplier;
            } else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space)){
                r2d.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

        //Defense
        if (Input.GetKeyDown(KeyCode.RightShift) && !isLadder)
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
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            isAttacking = true;
        }
    }

    // Jump
    void Jump()
    {
        isGrounded = false;
        isJump = true;
        CreateDust();
        _animator.SetTrigger("jump");
        _animator.SetBool("grounded", false);
        r2d.velocity = new Vector2(r2d.velocity.x, 0);
        r2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    void CreateDust()
    {
        dust.Play();
    }
}
