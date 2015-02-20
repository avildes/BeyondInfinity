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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.Instance.onObjectCollectedEvent(ObjectType.COIN);
            Destroy(gameObject);
        }
    }
}
