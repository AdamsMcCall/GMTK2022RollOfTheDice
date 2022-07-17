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
    public int goal = 20;
    private TextMeshProUGUI goaltext;
    public GameObject GameEnv;

    private void Start()
    {
        goaltext = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void AddToScore(int value)
    {
        score += value;
        if (score >= goal)
        {
            var levelManagement = GameEnv.GetComponent<LevelManagement>();
            levelManagement.GoToNextLevel();
        }
    }

    public void SubToScore(int value)
    {
        score -= value;
    }

    void Update()
    {
        goaltext.text = "Goal : " + goal;
    }
}