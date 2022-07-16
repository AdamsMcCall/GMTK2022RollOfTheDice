using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Additive);
    }
}
