using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DiceBehavior : MonoBehaviour
{
    private GridBehavior grid;
    public GameObject GridObject;
    public int grid_x = 0;
    public int grid_y = 0;
    public GameObject Cube;
    public GameObject CameraParent;
    private bool isRotating = false;
    private bool isTranslating = false;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GridObject.GetComponent(typeof(GridBehavior)) as GridBehavior; // ne fonctionne pas
        transform.position = new Vector3(grid_x, transform.position.y, grid_y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && grid_x > 0 && !isRotating && !isTranslating)
        {
            StartCoroutine(RotateDiceMeshRoutine(-0.5f, 0, -1, 0, Vector3.forward));
            StartCoroutine(TranslateCameraCoroutine(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && grid_x < grid.Width - 1 && !isRotating&& !isTranslating)
        {
            StartCoroutine(RotateDiceMeshRoutine(+0.5f, 0, 1, 0, Vector3.back));
            StartCoroutine(TranslateCameraCoroutine(1, 0));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grid_y < grid.Height - 1 && !isRotating&& !isTranslating)
        {
            StartCoroutine(RotateDiceMeshRoutine(0, +0.5f, 0, 1, Vector3.right));
            StartCoroutine(TranslateCameraCoroutine(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grid_y > 0 && !isRotating&& !isTranslating)
        {
            StartCoroutine(RotateDiceMeshRoutine(0, -0.5f, 0, -1, Vector3.left));
            StartCoroutine(TranslateCameraCoroutine(0, -1));

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

    IEnumerator TranslateCameraCoroutine(float x_pos,float z_pos)
    {
        isRotating = true;
        isTranslating = true;
        
        var startPosition = CameraParent.transform.position;
        var targetPosition = new Vector3(
            CameraParent.transform.position.x + x_pos,
            CameraParent.transform.position.y,
            CameraParent.transform.position.z + z_pos);
        var time = 0f;
        var duration = 0.2f;

        while (time < duration)
        {
            CameraParent.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        isTranslating = false;

        CameraParent.transform.position = targetPosition;
    }
}
