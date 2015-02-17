using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    public int health;

    public GameObject itemToDrop;

    void OnMouseDown()
    {
        health--;

        if (health <= 0)
        {
            EventManager.Instance.onObjectDestroyedEvent(ObjectType.ENEMY, false);

            Instantiate(itemToDrop, transform.position, transform.rotation);
            
            gameObject.SetActive(false);

            gameObject.GetComponent<ShowableObject>().StopAllCoroutines();

            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
