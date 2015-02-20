using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int maxHealth;

    private int health;

    public int distanceToMove = 11;

    public float playerSpeed = 5;

    private Vector3 target;

    private bool move;

    private bool alive;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;

        EventManager.onLevelReady += movePlayer;

        alive = true;
        health = maxHealth;
    }

    public void ResetPlayer()
    {
        alive = true;
        health = maxHealth;
        transform.position = initialPosition;

        
    }

    void OnDestroy()
    {
        EventManager.onLevelReady -= movePlayer;
    }

    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, (playerSpeed * Time.deltaTime));

            if (Vector3.Distance(transform.position, target) == 0)
            {
                EventManager.Instance.onPlayerReadyEvent();

                move = false;
            }
        }
    }

    public void movePlayer()
    {
        target = transform.position;
        target.z += distanceToMove;
        move = true;
    }

    public void DamagePlayer(int damage)
    {
        if (alive)
        {
            this.health -= damage;

            if (this.health <= 0)
            {
                EventManager.Instance.onPlayerDiedEvent();

                Debug.Log("player died");

                alive = false;
            }
        }
    }

    public bool IsHealthFull()
    {
        if (health == maxHealth) return true;
        else return false;
    }
}
