using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public List<Level> levels;

    private int actualLevel;

    private List<Vector2> occupiedPositions;

    private int maxColumns = 3;
    private int maxLines = 4;

    void Start()
    {
        actualLevel = -1;
    }

    void Update()
    {

    }

    public void CreateNextLevel()
    {
        actualLevel++;

        occupiedPositions = new List<Vector2>();

        if (actualLevel == levels.Count)
        {
            // end game
            // ShowScore
        }
        else
        {
            Vector2 objectPosition;

            GameObject obj;

            ///Instancia cada objeto do levelObject em uma das posicoes do grid
            foreach (LevelObject levelObject in levels[actualLevel].objects)
            {
                for (int i = 0; i < levelObject.quantity; i++)
                {
                    objectPosition = GetRandomEmptyPosition();

                    obj = Instantiate(levelObject.prefab) as GameObject;
                    obj.transform.parent = transform.GetChild((int) objectPosition.y).GetChild((int) objectPosition.x);
                    
                    obj.GetComponent<ShowableObject>().SetAppearanceSpeed(levels[actualLevel].appearanceSpeed);
                }
            }
        }

        EventManager.Instance.onLevelReadyEvent();
    }

    private Vector2 GetRandomEmptyPosition()
    {
        Vector2 ret;

        do
        {
            ret = GetRandomPosition();
        }
        while(occupiedPositions.Contains(ret));

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
    
}
