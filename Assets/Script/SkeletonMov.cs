using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkeletonMov : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //Minimum distance required to attack
    public float moveSpeed;
    public float cooldown;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hitZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //Distance b/w player and skeleton
    private bool attackMode;
    private bool cooling;
    private float initialTimer;
    #endregion


    private void Awake()
    {
        SelectTarget();
        initialTimer = cooldown; //Store the initial value of timer.
        anim = GetComponent<Animator>(); //caching animator
    }
   

    void Update()
    {
      
        if (!attackMode)
        {
            Move();
        }

        if(!InsideLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if(distance > attackDistance)
        {
            StopAttack();
        }else if(attackDistance > distance && !cooling)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("isAttack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        cooldown = initialTimer; //Reset cooldown
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("isAttack", true);
        
    }

    void Cooldown()
    {
        cooldown -= Time.deltaTime;

        if(cooldown <= 0 && cooling && attackMode)
        {
            cooling = false;
            cooldown = initialTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("isAttack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideLimits()
    {
        return (transform.position.x > leftLimit.transform.position.x) && (transform.position.x < rightLimit.transform.position.x);
    }

    public void SelectTarget()
    {
        float distanceLeft = Vector2.Distance(transform.position, leftLimit.transform.position);
        float distanceRight = Vector2.Distance(transform.position, rightLimit.transform.position);

        if(distanceLeft > distanceRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();

    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        } else
        {
            rotation.y = 0;
        }
        transform.eulerAngles = rotation;
    }
}
