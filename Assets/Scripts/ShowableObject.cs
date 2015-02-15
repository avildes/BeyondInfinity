using UnityEngine;
using System.Collections;

public class ShowableObject : MonoBehaviour
{
    private float appearanceSpeed;

    public int quantityOfAppearances;

    public float intervalBetweenAppearances;

    public void SetAppearanceSpeed(float value)
    {
        appearanceSpeed = value;
    }

	void Start ()
    {
        EventManager.onPlayerReady += ShowObject;
	}
	
    void OnDestroy()
    {
        EventManager.onPlayerReady -= ShowObject;
    }

	void Update ()
    {
	
	}

    void ShowObject()
    {

    }
}
