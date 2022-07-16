using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    private float timer = 1.5f;
    public Slider slider;
    public int score = 0;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1.5f;
            slider.value -= 10;
        }

        if (slider.value == 0)
        {
            
        }
    }
}