using UnityEngine;
using System.Collections;

public class SplashBehaviour : MonoBehaviour
{
    public GameObject nextScreen;

	void Update ()
    {
	    if(Input.touches.Length > 0 || Input.GetMouseButton(0))
        {
            nextScreen.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
