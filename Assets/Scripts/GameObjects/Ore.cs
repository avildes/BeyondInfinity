using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ore : MonoBehaviour
{
    public int health;

    public GameObject itemToDrop;

    void OnMouseDown()
    {
        health--;

        if (health <= 0)
        {
            EventManager.Instance.onObjectDestroyedEvent(ObjectType.ORE, false);

            Instantiate(itemToDrop, transform.position, transform.rotation);

            gameObject.SetActive(false);

            gameObject.GetComponent<ShowableObject>().StopAllCoroutines();

            Destroy(gameObject);
        }
    }
}
