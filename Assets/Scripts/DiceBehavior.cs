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
    public GameObject Cube;
    public GameObject CameraParent;
    private bool isRotating = false;
    private bool isTranslating = false;
    private DieFace currentFace;
    public PreviewPlaneBehavior previewFrontPlane;
    public PreviewPlaneBehavior previewRightPlane;
    public PreviewPlaneBehavior previewLeftPlane;
    public PreviewPlaneBehavior previewBackPlane;
    private bool startTileRemoved = false;

    public bool canMove => !isRotating && !isTranslating;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GridObject.GetComponent(typeof(GridBehavior)) as GridBehavior;
        transform.position = new Vector3(grid_x, transform.position.y, grid_y);
        currentFace = DieFace.GenerateDice();
        ShuffleDice();
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && grid_x > 0 && canMove && grid.IsTileAccessible(grid_x, grid_y, Direction.Left))
        {
            StartCoroutine(RotateDiceMeshRoutine(-0.5f, 0, Direction.Left, Vector3.forward));
            StartCoroutine(TranslateCameraCoroutine(Direction.Left));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && grid_x < grid.Width - 1 && canMove && grid.IsTileAccessible(grid_x, grid_y, Direction.Right))
        {
            StartCoroutine(RotateDiceMeshRoutine(+0.5f, 0, Direction.Right, Vector3.back));
            StartCoroutine(TranslateCameraCoroutine(Direction.Right));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grid_y < grid.Height - 1 && canMove && grid.IsTileAccessible(grid_x, grid_y, Direction.Up))
        {
            StartCoroutine(RotateDiceMeshRoutine(0, +0.5f, Direction.Up, Vector3.right));
            StartCoroutine(TranslateCameraCoroutine(Direction.Up));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grid_y > 0 && canMove && grid.IsTileAccessible(grid_x, grid_y, Direction.Down))
        {
            StartCoroutine(RotateDiceMeshRoutine(0, -0.5f, Direction.Down, Vector3.left));
            StartCoroutine(TranslateCameraCoroutine(Direction.Down));
        }

        if (!startTileRemoved)
        {
            grid.RemoveTile(grid_x, grid_y);
            startTileRemoved = true;
        }
    }

    private void CheckDisplayPreview()
    {
        previewLeftPlane.Display(grid.IsTileAccessible(grid_x, grid_y, Direction.Left));
        previewRightPlane.Display(grid.IsTileAccessible(grid_x, grid_y, Direction.Right));
        previewFrontPlane.Display(grid.IsTileAccessible(grid_x, grid_y, Direction.Up));
        previewBackPlane.Display(grid.IsTileAccessible(grid_x, grid_y, Direction.Down));
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

    private void RotateDiceInstant(float x_rot, float z_rot, Direction direction, Vector3 axis)
    {
        var point = new Vector3(
            Cube.transform.position.x + x_rot,
            Cube.transform.position.y - 0.5f,
            Cube.transform.position.z + z_rot);
        Cube.transform.RotateAround(point, axis, 90);
    }

    private void ShuffleDice()
    {
        for (int i = 0; i < 20; ++i)
        {
            var rng = Random.Range(0, 4);

            switch (rng)
            {
                case 0:
                    RotateDiceInstant(-0.5f, 0, Direction.Left, Vector3.forward);
                    currentFace = DirectionHelper.TurnDice(currentFace, Direction.Left);
                    break;
                case 1:
                    RotateDiceInstant(+0.5f, 0, Direction.Right, Vector3.back);
                    currentFace = DirectionHelper.TurnDice(currentFace, Direction.Right);
                    break;
                case 2:
                    RotateDiceInstant(0, +0.5f, Direction.Up, Vector3.right);
                    currentFace = DirectionHelper.TurnDice(currentFace, Direction.Up);
                    break;
                case 3:
                    RotateDiceInstant(0, -0.5f, Direction.Down, Vector3.left);
                    currentFace = DirectionHelper.TurnDice(currentFace, Direction.Down);
                    break;
            }
        }
        Cube.transform.position = transform.position;
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
        grid.RemoveTile(grid_x, grid_y);
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
