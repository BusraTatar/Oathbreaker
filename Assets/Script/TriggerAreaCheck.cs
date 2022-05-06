using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private SkeletonMov skeletonParent;

    private void Awake()
    {
        skeletonParent = GetComponentInParent<SkeletonMov>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            skeletonParent.target = other.transform;
            skeletonParent.inRange = true;
            skeletonParent.hitZone.SetActive(true);
        }
    }
}
