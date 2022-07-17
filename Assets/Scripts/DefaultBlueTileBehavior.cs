using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DefaultBlueTileBehavior : MonoBehaviour, ITile
{
    private GameObject Plaisir;
    private SliderControl SliderScript;
    private TextMeshProUGUI Textou;

    public void ApplyTileEffect(int x, int y, int value)
    {
        print($"Arrived on Blue tile at {x}, {y} with value {value}");
        SliderScript.slider.value -= value;
        SliderScript.score -= value;
        Textou.text = "Score :"+SliderScript.score;
    }

    public void Initialize(GameObject gameEnv)
    {
        Plaisir = gameEnv;
        SliderScript = Plaisir.GetComponentInChildren<SliderControl>();
        Textou = Plaisir.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}