using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private static Audio obje = null;

    void Awake()
    {
        if (obje == null)
        {
            obje = this;
            DontDestroyOnLoad(this);
        }
        else if (this != obje)
        {
            Destroy(gameObject);
        }
    }
}
