using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPlaneBehavior : MonoBehaviour
{
    public List<Material> DieFaceMaterials = new List<Material>();
    public Renderer RendererComponent;

    private void Start()
    {
    }

    public void ChangeFace(int number)
    {
        if (number > 0 && number <= 6)
        {
            if (RendererComponent != null)
            {
                RendererComponent.sharedMaterial = DieFaceMaterials[number - 1];
            }
        }
    }

    public void Initialize()
    {
        RendererComponent = GetComponent<Renderer>();
    }

    public void Display(bool value)
    {
        RendererComponent.enabled = value;
    }

    private void Update()
    {
        
    }
}   
