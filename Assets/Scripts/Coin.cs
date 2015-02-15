using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public float speed;

    private bool move;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, (speed * Time.deltaTime));
    }

    /*void OnMouseDown()
    {
        if (!move)
        {
            EventManager.Instance.onCoinCollectedEvent();
            move = true;
        }
    }
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
