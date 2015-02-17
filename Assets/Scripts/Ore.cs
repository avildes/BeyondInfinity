using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ore : MonoBehaviour
{
    public int health;

    public List<GameObject> itensToDrop;

    public List<int> quantities;

    void OnMouseDown()
    {
        health--;

        if (health <= 0)
        {
            EventManager.Instance.onObjectDestroyedEvent(ObjectType.ORE, false);

            for (int i = 0; i < itensToDrop.Count; i++)
            {
                for (int j = 0; j < quantities[i]; j++)
                {
                    Instantiate(itensToDrop[i], transform.position, transform.rotation);
                }
            }

            gameObject.SetActive(false);

            gameObject.GetComponent<ShowableObject>().StopAllCoroutines();

            Destroy(gameObject);
        }
    }
}
