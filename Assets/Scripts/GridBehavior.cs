using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public int Width = 20;

    public int Height = 10;

    public List<GameObject> TileTypes;

    public List<int> TileProbability;

    private List<ITile[]> TileGrid;

    private List<GameObject[]> TileGridObject;

    public GameObject GameEnvironment;

    public GameObject EmptyTile;

    private void Awake()
    {
        // + 0.5f needed for some reasons
        transform.position = new Vector3(transform.position.x - Width / 2f + 0.5f, transform.position.y, transform.position.z - Height / 2f + 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        TileGrid = new List<ITile[]>();
        TileGridObject = new List<GameObject[]>();

        foreach (GameObject obj in TileTypes)
        {
            var tileComponent = obj.GetComponent(typeof(ITile));

            if (tileComponent == null)
            {
                TileTypes.Remove(obj);
            }
        }

        for (int y = 0; y < Height; ++y)
        {
            TileGrid.Add(new ITile[Width]);
            TileGridObject.Add(new GameObject[Width]);

            for (int x = 0; x < Width; ++x)
            {
                InsertNewTile(x, y);
            }
        }
    }

    private void InsertNewTile(int x, int y)
    {
        GameObject obj;

        if (TileTypes.Count != TileProbability.Count)
        {
            obj = Instantiate(TileTypes[Random.Range(0, TileTypes.Count)], new Vector3(transform.position.x + x, 0, transform.position.z + y), Quaternion.identity, transform);
        }
        else
        {
            obj = Instantiate(GetTileTypeFromProbability(), new Vector3(transform.position.x + x, 0, transform.position.z + y), Quaternion.identity, transform);
        }

        TileGridObject[y][x] = obj;
        var tile = obj.GetComponent(typeof(ITile)) as ITile;
        tile.Initialize(GameEnvironment);
        TileGrid[y][x] = tile;
    }

    private GameObject GetTileTypeFromProbability()
    {
        var totalProbability = 0;
        TileProbability.ForEach(x => totalProbability += x);
        var randomNumber = Random.Range(0, totalProbability);
        int i = 0;
        int j = 0;

        foreach (int probability in TileProbability)
        {
            if (randomNumber >= i && randomNumber < i + probability)
            {
                return TileTypes[j];
            }
            i += probability;
            ++j;
        }
        return TileTypes[Random.Range(0, TileTypes.Count)];
    }

    public void RemoveTile(int x, int y)
    {
        var obj = Instantiate(EmptyTile, new Vector3(transform.position.x + x, 0, transform.position.z + y), Quaternion.identity, transform);

        Destroy(TileGridObject[y][x]);
        TileGridObject[y][x] = obj;
        var tile = obj.GetComponent(typeof(ITile)) as ITile;
        tile.Initialize(GameEnvironment);
        TileGrid[y][x] = tile;
    }

    public bool IsTileAccessible(int x, int y, Direction direction)
    {
        try
        {
            var directionVector = DirectionHelper.GetVectorFromDirection(direction);

            if (x + (int)directionVector.x < 0 || x + (int)directionVector.x >= Width ||
                y + (int)directionVector.y < 0 || y + (int)directionVector.y >= Height)
            {
                return false;
            }

            return TileGrid[y + (int)directionVector.y][x + (int)directionVector.x].isAccessible;
        }
        catch (System.Exception)
        {
            return false;
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

    public void GenerateNewLine()
    {
        TileGrid.Add(new ITile[Width]);

        for (int x = 0; x < Width; ++x)
        {
            InsertNewTile(x, TileGrid.Count - 1);
        }
        Height += 1;
    }

    public void ExecuteTile(int x, int y, int value)
    {
        TileGrid[y][x].ApplyTileEffect(x, y, value);

    }
}
