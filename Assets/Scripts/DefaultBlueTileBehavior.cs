using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DefaultBlueTileBehavior : MonoBehaviour, ITile
{
    private GameObject Plaisir;
    private Scorekeeper scorekeeperscript;
    private TextMeshProUGUI Textou;

    public bool isAccessible => true;

    public void ApplyTileEffect(int x, int y, int value)
    {
        print($"Arrived on Blue tile at {x}, {y} with value {value}");
        scorekeeperscript.score -= value;
        Textou.text = "Score :" + scorekeeperscript.score;
    }

    public void Initialize(GameObject gameEnv)
    {
        Plaisir = gameEnv;
        scorekeeperscript = Plaisir.GetComponentInChildren<Scorekeeper>();
        Textou = Plaisir.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}