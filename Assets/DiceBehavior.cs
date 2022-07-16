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
    private bool isRotating = false;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GridObject.GetComponent(typeof(GridBehavior)) as GridBehavior; // ne fonctionne pas
        transform.position = new Vector3(grid_x, transform.position.y, grid_y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && grid_x > 0 && !isRotating)
        {
            StartCoroutine(RotateDiceMeshRoutine(-0.5f, 0, -1, 0, Vector3.forward));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && grid_x < grid.Width - 1 && !isRotating)
        {
            StartCoroutine(RotateDiceMeshRoutine(+0.5f, 0, 1, 0, Vector3.back));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grid_y < grid.Height - 1 && !isRotating)
        {
            StartCoroutine(RotateDiceMeshRoutine(0, +0.5f, 0, 1, Vector3.right));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grid_y > 0 && !isRotating)
        {
            StartCoroutine(RotateDiceMeshRoutine(0, -0.5f, 0, -1, Vector3.left));
        }
    }

    IEnumerator RotateDiceMeshRoutine(float x_rot, float z_rot, int x_pos, int z_pos, Vector3 axis)
    {
        isRotating = true;
        int threshold = 1;
        var point = new Vector3(
            Cube.transform.position.x + x_rot,
            Cube.transform.position.y - 0.5f,
            Cube.transform.position.z + z_rot);
        for (float i = 0; i < 90; i += threshold)
        {
            Cube.transform.RotateAround(point, axis, threshold);
            yield return null;
        }

        Cube.transform.position = transform.position;
        transform.position = new Vector3(transform.position.x + x_pos, transform.position.y, transform.position.z + z_pos);
        grid_x += x_pos;
        grid_y += z_pos; // Calling it Y instead of Z because grid is 2D
        grid.GetGridValue(grid_x, grid_y);
        isRotating = false;
    }
}
