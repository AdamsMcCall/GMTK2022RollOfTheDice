using Assets.Scipts;
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
    private int max_grid_y = 0;
    public GameObject Cube;
    public GameObject CameraParent;
    private bool isRotating = false;
    private bool isTranslating = false;
    private DieFace currentFace;
    public PreviewPlaneBehavior previewFrontPlane;
    public PreviewPlaneBehavior previewRightPlane;
    public PreviewPlaneBehavior previewLeftPlane;
    public PreviewPlaneBehavior previewBackPlane;

    private bool canMove => !isRotating && !isTranslating;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GridObject.GetComponent(typeof(GridBehavior)) as GridBehavior;
        transform.position = new Vector3(grid_x, transform.position.y, grid_y);
        currentFace = DieFace.GenerateDice();
        previewFrontPlane.Initialize();
        previewFrontPlane.ChangeFace(currentFace.Up.Value);
        previewRightPlane.Initialize();
        previewRightPlane.ChangeFace(currentFace.Right.Value);
        previewLeftPlane.Initialize();
        previewLeftPlane.ChangeFace(currentFace.Left.Value);
        previewBackPlane.Initialize();
        previewBackPlane.ChangeFace(currentFace.Down.Value);
        CheckDisplayPreview();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDisplayPreview();
        if (Input.GetKeyDown(KeyCode.LeftArrow) && grid_x > 0 && canMove)
        {
            StartCoroutine(RotateDiceMeshRoutine(-0.5f, 0, Direction.Left, Vector3.forward));
            StartCoroutine(TranslateCameraCoroutine(Direction.Left));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && grid_x < grid.Width - 1 && canMove)
        {
            StartCoroutine(RotateDiceMeshRoutine(+0.5f, 0, Direction.Right, Vector3.back));
            StartCoroutine(TranslateCameraCoroutine(Direction.Right));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grid_y < grid.Height - 1 && canMove)
        {
            StartCoroutine(RotateDiceMeshRoutine(0, +0.5f, Direction.Up, Vector3.right));
            StartCoroutine(TranslateCameraCoroutine(Direction.Up));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grid_y > 0 && canMove)
        {
            StartCoroutine(RotateDiceMeshRoutine(0, -0.5f, Direction.Down, Vector3.left));
            StartCoroutine(TranslateCameraCoroutine(Direction.Down));
        }

        if (grid_y > max_grid_y)
        {
            grid.GenerateNewLine();
            max_grid_y = grid_y;
        }
    }

    private void CheckDisplayPreview()
    {
        previewLeftPlane.Display(grid_x > 0);
        previewRightPlane.Display(grid_x < grid.Width - 1);
        previewFrontPlane.Display(grid_y < grid.Height - 1);
        previewBackPlane.Display(grid_y > 0);
    }

    IEnumerator RotateDiceMeshRoutine(float x_rot, float z_rot, Direction direction, Vector3 axis)
    {
        isRotating = true;

        int angle = 2;
        var point = new Vector3(
            Cube.transform.position.x + x_rot,
            Cube.transform.position.y - 0.5f,
            Cube.transform.position.z + z_rot);
        for (float i = 0; i < 90; i += angle)
        {
            Cube.transform.RotateAround(point, axis, angle);
            yield return null;
        }

        
        isRotating = false;
        
        ValidatePosition(direction);
    }

    void ValidatePosition(Direction direction)
    {
        if (!canMove)
        {
            return;
        }

        var directionVector = DirectionHelper.GetVectorFromDirection(direction);
        Cube.transform.position = transform.position;
        CameraParent.transform.position = transform.position;
        transform.position = new Vector3(transform.position.x + directionVector.x, transform.position.y, transform.position.z + directionVector.y);
        grid_x += (int)directionVector.x;
        grid_y += (int)directionVector.y;

        currentFace = DirectionHelper.TurnDice(currentFace, direction);
        previewFrontPlane.ChangeFace(currentFace.Up.Value);
        previewRightPlane.ChangeFace(currentFace.Right.Value);
        previewLeftPlane.ChangeFace(currentFace.Left.Value);
        previewBackPlane.ChangeFace(currentFace.Down.Value);
        
        grid.ExecuteTile(grid_x, grid_y, currentFace.Value);
    }

    IEnumerator TranslateCameraCoroutine(Direction direction)
    {
        isTranslating = true;

        var directionVector = DirectionHelper.GetVectorFromDirection(direction);
        var startPosition = CameraParent.transform.position;
        var targetPosition = new Vector3(
            CameraParent.transform.position.x + directionVector.x,
            CameraParent.transform.position.y,
            CameraParent.transform.position.z + directionVector.y);
        var time = 0f;
        var duration = 0.15f;

        while (time < duration)
        {
            CameraParent.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        isTranslating = false;

        ValidatePosition(direction);
    }
}
