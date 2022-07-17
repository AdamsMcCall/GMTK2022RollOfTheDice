using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
