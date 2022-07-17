using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource audiodata;
    public DiceBehavior dicy;
    private void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && dicy.canMove && !audiodata.isPlaying)
        {
            audiodata.Play(0);
        }

    }
}