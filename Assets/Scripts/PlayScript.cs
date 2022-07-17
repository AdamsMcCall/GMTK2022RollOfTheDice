using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{
    public Button playButton;
    public Button quitbutton;
    

    private void Start()
    {
        playButton.onClick.AddListener(play);
        quitbutton.onClick.AddListener(Quit);
    }

    public void play()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
