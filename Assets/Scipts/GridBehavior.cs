using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public int Width = 20;

    public int Height = 10;

    public List<GameObject> TileTypes;

    private List<ITile> Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new List<ITile>(Width * Height);

        foreach (GameObject obj in TileTypes)
        {
            var tileComponent = obj.GetComponent(typeof(ITile));

            if (tileComponent == null)
            {
                TileTypes.Remove(obj);
            }
        }

        for (int x = 0; x < Width; ++x)
        {
            for (int y = 0; y < Height; ++y)
            {
                var obj = Instantiate(TileTypes[Random.Range(0, TileTypes.Count)], new Vector3(x, 0, y), Quaternion.identity, transform);
                Grid.Add(obj.GetComponent(typeof(ITile)) as ITile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetGridValue(int x, int y)
    {
        print($"get info on {x}, {y};");
    }
}
