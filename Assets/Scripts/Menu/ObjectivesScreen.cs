using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectivesScreen : MonoBehaviour
{
    public Text planetName;

    //public Image

    public Text objective1_label;

    //public Image

    public Text objective1_value;

    public Text objective2_label;

    //public Image

    public Text objective2_value;

    public void LoadLevel()
    {
        Application.LoadLevel(planetName.text);
    }
}
