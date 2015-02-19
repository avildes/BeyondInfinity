using UnityEngine;
using System.Collections;

public class PersistenceHelper : MonoBehaviour
{

    public static PersistenceHelper Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void SaveIntToPlayerPrefs(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public int GetIntPlayerPrefs(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetInt(key);
        else
            return 0;
    }
}
