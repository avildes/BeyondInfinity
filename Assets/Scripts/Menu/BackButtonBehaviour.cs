using UnityEngine;
using System.Collections;

public class BackButtonBehaviour : MonoBehaviour
{
    public void onClick()
    {
        Destroy(transform.parent.gameObject);
    }
}
