using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<LevelObjective> phaseObjectives;

    public List<Level> levels;

    private Hashtable achievedObjectives;
    private Dictionary<string, int> statistics;

    private int actualLevel;

    private List<Vector2> occupiedPositions;

    private int maxColumns = 3;
    private int maxLines = 4;

    private int objectCount;

    void Start()
    {
        EventManager.onObjectDestroyed += onObjectDestroyed;
        EventManager.onPlayerDied += onPlayerDie;
        EventManager.onObjectCollected += onObjectCollected;

        StartGame();
    }

    void StartGame()
    {
        actualLevel = -1;

        achievedObjectives = new Hashtable();
        
        statistics = new Dictionary<string, int>();

        CreateNextLevel();
    }

    void OnDestroy()
    {
        EventManager.onObjectDestroyed -= onObjectDestroyed;
        EventManager.onPlayerDied -= onPlayerDie;
        EventManager.onObjectCollected -= onObjectCollected;
    }

    #region utils - random

    private Vector2 GetRandomEmptyPosition()
    {
        Vector2 ret;

        do
        {
            ret = GetRandomPosition();
        }
        while (occupiedPositions.Contains(ret));

        occupiedPositions.Add(ret);

        return ret;
    }

    private Vector2 GetRandomPosition()
    {
        float randomNumber;

        int line, column;

        randomNumber = Random.value;

        line = Mathf.FloorToInt(randomNumber * maxLines);

        randomNumber = Random.value;

        column = Mathf.FloorToInt(randomNumber * maxColumns);

        return new Vector2(line, column);
    }

    #endregion

    #region level and phase controllers

    public void CreateNextLevel()
    {
        actualLevel++;

        occupiedPositions = new List<Vector2>();

        if (actualLevel == levels.Count)
        {
            EndPhase();
        }
        else
        {
            Vector2 objectPosition;

            GameObject obj;

            objectCount = 0;

            Vector3 position = transform.position;
            position.z += 11;
            transform.position = position;

            
            ///Instancia cada objeto do levelObject em uma das posicoes do grid
            foreach (LevelObject levelObject in levels[actualLevel].objects)
            {
                for (int i = 0; i < levelObject.quantity; i++)
                {
                    objectPosition = GetRandomEmptyPosition();

                    obj = Instantiate(levelObject.prefab) as GameObject;
                    Transform parent = transform.GetChild((int)objectPosition.y).GetChild((int)objectPosition.x).GetChild(0); // coluna, linha, groundtile
                    obj.transform.parent = parent;

                    obj.transform.localPosition = new Vector3(0, -2, 0); // parent.position;
                    

                    obj.GetComponent<ShowableObject>().SetAppearanceSpeed(levels[actualLevel].appearanceSpeed);

                    objectCount++;
                }
            }

            EventManager.Instance.onLevelReadyEvent();
        }
    }
    /*
    private void ClearRotations()
    {
        Transform tile;

        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(0).childCount; j++)
            {
                tile = transform.GetChild(i).GetChild(j).GetChild(0);
                tile.rotation = Quaternion.identity;
            }
        }
    }
    */
    private void CheckObjectCount()
    {
        objectCount--;

        if (objectCount <= 0)
        {
            StartCoroutine(WaitAndCreateNextLevel());
        }
    }

    IEnumerator WaitAndCreateNextLevel()
    {
        yield return new WaitForSeconds(1);
        CreateNextLevel();
    }

    private void EndPhase()
    {
        int stars = 0;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(player.GetComponent<Player>().IsHealthFull())
        {
            stars++;
        }

        foreach (LevelObjective objective in phaseObjectives)
        {
            if (achievedObjectives.ContainsKey(objective.type))
            {
                if(objective.quantity <= (int) achievedObjectives[objective.type])
                {
                    stars++;
                }
            }
        }

        StartCoroutine(SaveStatistics());

        Debug.Log("stars: " + stars);
    }

    #endregion

    #region event listeners

    private void onObjectCollected(ObjectType type)
    {
        string key = type.ToString();

        if (achievedObjectives.ContainsKey(type))
        {
            int qtd = (int)achievedObjectives[type];
            qtd++;
            achievedObjectives[type] = qtd;
        }
        else
        {
            achievedObjectives.Add(type, 1);
        }

        IncrementValueStatistics(key);
    }

    private void onObjectDestroyed(ObjectType type, bool timeout)
    {
        string key = type.ToString();
        
        if (timeout)
        {
            key += "Missed";
        }
        else
        {
            if (achievedObjectives.ContainsKey(type))
            {
                int qtd = (int)achievedObjectives[type];
                qtd++;
                achievedObjectives[type] = qtd;
            }
            else
            {
                achievedObjectives.Add(type, 1);
            }
        }

        IncrementValueStatistics(key);
        CheckObjectCount();
    }

    private void onPlayerDie()
    {
        EndPhase();
    }
    #endregion

    #region Statistics

    private IEnumerator SaveStatistics()
    {
        foreach (KeyValuePair<string, int> entry in statistics)
        {
            PersistenceHelper.Instance.SaveIntToPlayerPrefs(entry.Key, entry.Value);
        }

        yield return new WaitForEndOfFrame();
    }

    private void IncrementValueStatistics(string key)
    {
        if(statistics.ContainsKey(key))
        {
            statistics[key] += 1;
        }
        else
        {
            statistics.Add(key, 1);
        }
    }

    #endregion
}
