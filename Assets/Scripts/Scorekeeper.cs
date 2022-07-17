using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour
{
    public int score = 0;
    public int goal = 10;
    private TextMeshProUGUI goaltext;
    private void Start()
    {
        goaltext = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        goaltext.text = "goal : " + goal;
    }
}