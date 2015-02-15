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

    void Start()
    {
        alive = true;

        move = false;

        EventManager.onLevelReady += movePlayer;

        health = maxHealth;
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
        move = true;

        target = transform.position;
        target.z += distanceToMove;
    }

    public void DamagePlayer(int damage)
    {
        if (alive)
        {
            this.health -= damage;

            if (this.health <= 0)
            {
                EventManager.Instance.onPlayerDiedEvent();
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
