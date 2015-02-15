using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int distanceToMove = 11;

    public float playerSpeed = 5;

    private Vector3 target;

    private bool move;

    void Start()
    {
        move = false;

        EventManager.onLevelReady += movePlayer;
    }
    
    void OnDestroy()
    {
        EventManager.onLevelReady -= movePlayer;
    }

    void Update()
    {
        if(move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, (playerSpeed * Time.deltaTime));

            if(Vector3.Distance(transform.position, target) == 0)
            {
                EventManager.Instance.onPlayerReadyEvent();

                move = false;
            }
        }
    }

    public void movePlayer()
    {
        move = true;

        target = transform.position;
        target.z += distanceToMove;
    }
}
