using UnityEngine;
using System.Collections;

public class ListItemBehaviour : MonoBehaviour
{
    public Phase phase;

    //private ObjectivesScreen objectivesScreen;

    public GameObject screenPrefab;

    void Start()
    {
        //objectivesScreenGameObject = GameObject.FindGameObjectWithTag("ObjectivesScreen");
        //objectivesScreen = objectivesScreenGameObject.GetComponent<ObjectivesScreen>();
    }

    public void onClick()
    {
        GameObject screenObj = Instantiate(screenPrefab) as GameObject;

        ObjectivesScreen objectivesScreen = screenObj.GetComponent<ObjectivesScreen>();

        objectivesScreen.planetName.text = phase.name;
        
        objectivesScreen.objective1_label.text = phase.objective1.name;
        objectivesScreen.objective1_value.text = phase.objective1.quantity.ToString();

        objectivesScreen.objective2_label.text = phase.objective2.name;
        objectivesScreen.objective2_value.text = phase.objective2.quantity.ToString();
        
        screenObj.SetActive(true);
    }
}
