using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayAgainButtonscripts : MonoBehaviour
{
    public Button replayButton;
    public Button quitbutton;
    

    private void Start()
    {
        replayButton.onClick.AddListener(Replay);
        quitbutton.onClick.AddListener(Quit);
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
