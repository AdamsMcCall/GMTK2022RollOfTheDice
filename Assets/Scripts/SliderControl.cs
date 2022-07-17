using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public float timerDuration = 0.5f;
    private float remainingTime;
    public Slider slider;
    public int score = 0;

    private void Start()
    {
        remainingTime = timerDuration;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = timerDuration;
            slider.value -= 2;
        }

        if (slider.value == 0)
        {
            //SceneManager.LoadScene("GameoverScreen", LoadSceneMode.Single);
        }
    }
}