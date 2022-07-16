using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehavior : MonoBehaviour
{
    public List<Material> mats = new List<Material>();
    private Renderer rend;
    public int i = 0;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = mats[i];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (i == 5)
            {
                i = 0;
            }
            
            i = i+1;
            
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = mats[i];
            
        }
    }
}   
