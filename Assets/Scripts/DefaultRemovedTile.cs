using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRemovedTile : MonoBehaviour, ITile
{
    private GameObject Plaisir;
    public bool isAccessible => false;

    public void ApplyTileEffect(int x, int y, int value)
    {
        // Do nothing
    }

    public void Initialize(GameObject gameEnv)
    {
        Plaisir = gameEnv;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
