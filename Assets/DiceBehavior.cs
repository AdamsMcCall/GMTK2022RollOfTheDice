using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehavior : MonoBehaviour
{
    private GridBehavior grid;
    public GameObject GridObject;
    public int grid_x = 0;
    public int grid_y = 0;
    public GameObject Cube;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GridObject.GetComponent(typeof(GridBehavior)) as GridBehavior; // ne fonctionne pas
        transform.position = new Vector3(grid_x, transform.position.y, grid_y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && grid_x > 0)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            Cube.transform.RotateAround(
                new Vector3(
                    Cube.transform.position.x - 0.5f,
                    Cube.transform.position.y - 0.5f,
                    Cube.transform.position.z),
                Vector3.forward, 
                90f);
            grid_x -= 1;
            grid.GetGridValue(grid_x, grid_y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && grid_x < grid.Width - 1)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            Cube.transform.RotateAround(
                new Vector3(
                    Cube.transform.position.x + 0.5f,
                    Cube.transform.position.y - 0.5f,
                    Cube.transform.position.z),
                Vector3.back, 
                90f);
            grid_x += 1;
            grid.GetGridValue(grid_x, grid_y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grid_y < grid.Height - 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            Cube.transform.RotateAround(
                new Vector3(
                    Cube.transform.position.x,
                    Cube.transform.position.y - 0.5f,
                    Cube.transform.position.z + 0.5f),
                Vector3.right, 
                90f);
            grid_y += 1;
            grid.GetGridValue(grid_x, grid_y);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grid_y > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Cube.transform.RotateAround(
                new Vector3(
                    Cube.transform.position.x,
                    Cube.transform.position.y - 0.5f,
                    Cube.transform.position.z - 0.5f),
                Vector3.left, 
                90f);
            grid_y -= 1;
            grid.GetGridValue(grid_x, grid_y);
        }
    }
}
