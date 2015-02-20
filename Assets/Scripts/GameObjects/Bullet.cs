using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;

    private bool move;

    private Transform target;

    public int damage;

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
            target.gameObject.GetComponent<Player>().DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
}
