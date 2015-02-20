using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapBehaviour : MonoBehaviour
{
    public GameObject contentHolder;
    
    public GameObject planetPrefab;

    public PhaseManager phaseManager;

    private List<Phase> phaseList;

	void Start ()
    {
        phaseList = phaseManager.GetPhaseList();

        PopulateContent();
	}
	
    private void PopulateContent()
    {
        foreach (Phase phase in phaseList)
        {
            GameObject planet = Instantiate(planetPrefab) as GameObject;

            planet.transform.GetChild(0).GetComponent<Text>().text = phase.name;
            planet.transform.GetChild(1).GetComponent<Text>().text = phase.description;
            planet.GetComponent<ListItemBehaviour>().phase = phase;

            planet.transform.SetParent(contentHolder.transform, false);
        }
    }
}
