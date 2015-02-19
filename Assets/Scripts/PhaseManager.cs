using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhaseManager : MonoBehaviour
{
    public List<Phase> phaseList;

    public static PhaseManager Instance;

	public Phase GetPhase(int value)
    {
        return phaseList[value];
	}

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
