using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneCheck : MonoBehaviour
{
    private SkeletonMov skeletonParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        skeletonParent = GetComponentInParent<SkeletonMov>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack"))
        {
            skeletonParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            skeletonParent.triggerArea.SetActive(true);
            skeletonParent.inRange = false;
            skeletonParent.SelectTarget();
        }
    }
}
