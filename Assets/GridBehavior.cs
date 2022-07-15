using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public int Width = 20;

    public int Height = 10;

    public GameObject DefaultFloor;

    private List<GameObject> Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new List<GameObject>(Width * Height);

        for (int x = 0; x < Width; ++x)
        {
            for (int y = 0; y < Height; ++y)
            {
                Grid.Add(Instantiate(DefaultFloor, new Vector3(x, 0, y), Quaternion.identity, transform));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
