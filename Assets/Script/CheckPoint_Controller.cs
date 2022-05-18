using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterMov._lastTransform = transform.position;
        }
    }
}
