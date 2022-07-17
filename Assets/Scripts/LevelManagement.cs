using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public List<GameObject> Levels;
    public GameObject LevelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel()
    {
        GameObject currentLevel = Levels.First();
        GameObject nextLevel = Levels.Last();

        var currentDice = currentLevel.GetComponentInChildren<DiceBehavior>();
        var nextDice = nextLevel.GetComponentInChildren<DiceBehavior>();
        nextDice.Initialize();
        currentDice.preventMove = true;

        StartCoroutine(TranslateLevelRoutine(currentLevel, false));
        StartCoroutine(TranslateLevelRoutine(nextLevel, true));
    }

    void OnTranslateRoutineDone(GameObject level)
    {
        var nextDice = level.GetComponentInChildren<DiceBehavior>();
        nextDice.preventMove = false;

        var newLevel = Instantiate(LevelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 20), Quaternion.identity, transform);
        var grid = newLevel.GetComponentInChildren<GridBehavior>();
        //Increase difficulty
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
