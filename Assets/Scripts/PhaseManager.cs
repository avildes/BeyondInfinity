using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    public List<Phase> phaseList;

	public Phase GetPhase(int value)
    {
        if (phaseList.Count > value)
            return phaseList[value];
        else
            return null;
	}

    public List<Phase> GetPhaseList()
    {
        return phaseList;
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
