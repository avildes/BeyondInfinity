using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreScreen;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public GameObject coins_value;

    public GameObject objective1_label;
    public GameObject objective1_value;
    public GameObject objective1_cleared;

    public GameObject objective2_label;
    public GameObject objective2_value;
    public GameObject objective2_cleared;

    private Hashtable achievedObjectives;

    private Phase phase;

    #region Buttons

    public void Retry()
    {
        EventManager.Instance.onRetryEvent();
    }

    public void NextLevel()
    {

    }

    public void BackToMap()
    {
        Application.LoadLevel("Splash+Maps");
    }

    #endregion

    public void ShowScore(int stars, int wallet, bool objective1Cleared, bool objective2Cleared, Hashtable achievedObjectives, Phase actualPhase)
    {
        this.achievedObjectives = achievedObjectives;
        this.phase = actualPhase;

        StartCoroutine(ShowScore(stars, wallet, objective1Cleared, objective2Cleared));
    }

    private IEnumerator ShowScore(int stars, int wallet, bool objective1Cleared, bool objective2Cleared)
    {
        scoreScreen.SetActive(true);

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        switch (stars)
        {
            case 1:
                star1.SetActive(true);
                break;
            case 2:
                star1.SetActive(true);
                yield return new WaitForEndOfFrame();
                star2.SetActive(true);
                break;
            case 3:
                star1.SetActive(true);
                yield return new WaitForEndOfFrame();
                star2.SetActive(true);
                yield return new WaitForEndOfFrame();
                star3.SetActive(true);
                break;
        }

        yield return new WaitForEndOfFrame();

        // animacao do dinheiro entrando
        coins_value.GetComponent<Text>().text = wallet.ToString();

        //set objective 1
        //name
        objective1_label.GetComponent<Text>().text = phase.objective1.name;

        //quantity
        if (achievedObjectives.ContainsKey(phase.objective1.type))
        {
            objective1_value.GetComponent<Text>().text = achievedObjectives[phase.objective1.type].ToString();
        }
        else
        {
            objective1_value.GetComponent<Text>().text = "0";
        }
        // check
        if (objective1Cleared)
        {
            objective1_cleared.SetActive(true);
            yield return new WaitForEndOfFrame();
        }

        //set objective 2
        //name
        objective2_label.GetComponent<Text>().text = phase.objective2.name;

        //quantity
        if (achievedObjectives.ContainsKey(phase.objective2.type))
        {
            objective2_value.GetComponent<Text>().text = achievedObjectives[phase.objective2.type].ToString();
        }
        else
        {
            objective2_value.GetComponent<Text>().text = "0";
        }

        //check
        if (objective2Cleared)
        {
            objective2_cleared.SetActive(true);
            yield return new WaitForEndOfFrame();
        }

    }

}
