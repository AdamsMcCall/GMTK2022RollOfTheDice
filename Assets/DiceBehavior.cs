using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehavior : MonoBehaviour
{
    private GridBehavior grid;
    public GameObject GridObject;
    public int grid_x = 0;
    public int grid_y = 0;

    // Start is called before the first frame update
    void Start()
    {
        grid = GridObject.GetComponent(typeof(GridBehavior)) as GridBehavior; // ne fonctionne pas
        transform.position = new Vector3(grid_x, 0, grid_y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && grid_x > 0)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            grid_x -= 1;
            grid.GetGridValue(grid_x, grid_y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && grid_x < grid.Width - 1)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            grid_x += 1;
            grid.GetGridValue(grid_x, grid_y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grid_y < grid.Height - 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            grid_y += 1;
            grid.GetGridValue(grid_x, grid_y);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grid_y > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            grid_y -= 1;
            grid.GetGridValue(grid_x, grid_y);
        }
    }
}
