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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            grid_x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            grid_x += 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            grid_y += 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            grid_y -= 1;
        }
        //grid.GetGridValue(grid_x, grid_y);
    }
}
