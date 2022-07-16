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
        RendererComponent = GetComponent<Renderer>();
        RendererComponent.sharedMaterial = null;
    }

    public void ChangeFace(int number)
    {
        if (number > 0 && number <= 6)
        {
            RendererComponent.sharedMaterial = DieFaceMaterials[number - 1];
        }
    }

    private void Update()
    {
        
    }
}   
