using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public List<GameObject> Levels;
    public GameObject LevelPrefab;
    private Scorekeeper scoreKeeper;
    private int levelNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GetComponentInChildren<Scorekeeper>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GoToNextLevel()
    {
        GameObject currentLevel = Levels.First();
        GameObject nextLevel = Levels.Last();

        var currentDice = currentLevel.GetComponentInChildren<DiceBehavior>();
        var nextDice = nextLevel.GetComponentInChildren<DiceBehavior>();
        nextDice.Initialize();
        currentDice.preventMove = true;

        levelNumber += 1;

        StartCoroutine(TranslateLevelRoutine(currentLevel, false));
        StartCoroutine(TranslateLevelRoutine(nextLevel, true));
    }

    void OnTranslateRoutineDone(GameObject level)
    {
        var nextDice = level.GetComponentInChildren<DiceBehavior>();
        nextDice.preventMove = false;

        var newLevel = Instantiate(LevelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 20), Quaternion.identity, transform);
        var grid = newLevel.GetComponentInChildren<GridBehavior>();

        scoreKeeper.goal += 40;
        if (levelNumber > 0 && levelNumber <= 5)
        {
            grid.Width = 5;
            grid.Height = 5;
            grid.TileProbability[0] = 6;
            grid.TileProbability[1] = 2;
            grid.TileProbability[2] = 0;
            grid.TileProbability[3] = 0;
        }
        else if (levelNumber > 5 && levelNumber <= 10)
        {
            grid.Width = 6;
            grid.Height = 6;
            grid.TileProbability[0] = 6;
            grid.TileProbability[1] = 3;
            grid.TileProbability[2] = 0;
            grid.TileProbability[3] = 1;
        }
        else if (levelNumber > 10 && levelNumber <= 15)
        {
            grid.Width = 7;
            grid.Height = 7;
            grid.TileProbability[0] = 6;
            grid.TileProbability[1] = 4;
            grid.TileProbability[2] = 0;
            grid.TileProbability[3] = 2;
        }
        else if (levelNumber > 15 && levelNumber <= 20)
        {
            grid.Width = 8;
            grid.Height = 8;
            grid.TileProbability[0] = 6;
            grid.TileProbability[1] = 5;
            grid.TileProbability[2] = 0;
            grid.TileProbability[3] = 3;
        }
        else
        {
            grid.Width = 9;
            grid.Height = 9;
            grid.TileProbability[0] = 6;
            grid.TileProbability[1] = 6;
            grid.TileProbability[2] = 0;
            grid.TileProbability[3] = 4;
        }
       

        grid.GameEnvironment = this.gameObject;
        grid.GenerateGrid();
        Levels.Add(newLevel);
    }

    IEnumerator TranslateLevelRoutine(GameObject level, bool isNextLevel)
    {
        var startPosition = level.transform.position;
        var targetPosition = new Vector3(
            level.transform.position.x,
            level.transform.position.y,
            level.transform.position.z - 20);
        var time = 0f;
        var duration = 2f;

        while (time < duration)
        {
            level.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        if (isNextLevel)
            OnTranslateRoutineDone(level);
        else
        {
            Levels.Remove(level);
            Destroy(level);
        }
    }
}
