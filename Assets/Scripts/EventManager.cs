using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private bool gamePaused = false;

    //-----EVENTS-----------------------------------------
    public delegate void GameOverHandler();
    public static event GameOverHandler onGameOver;

    public delegate void PauseHandler(bool paused);
    public static event PauseHandler onPause;

    public delegate void RetryHandler();
    public static event RetryHandler onRetry;

    public delegate void CoinCollectedHandler();
    public static event CoinCollectedHandler onCoinCollected;

    public delegate void EnemyHandler();
    public static event EnemyHandler onEnemyDie;

    public delegate void LevelHandler();
    public static event LevelHandler onLevelReady;

    public delegate void PlayerHandler();
    public static event PlayerHandler onPlayerReady;
    //----------------------------------------------------

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void onPauseEvent()
    {
        Debug.Log("paused");
        gamePaused = !gamePaused;
        onPause(gamePaused);
    }

    public void onGameOverEvent()
    {
        onGameOver();
    }

    public void onRetryEvent()
    {
        onRetry();
    }

    public void onCoinCollectedEvent()
    {
        Debug.Log("coin collected");
        //onCoinCollected();
    }
    
    public void onEnemyDieEvent()
    {
        Debug.Log("enemy destroyed");
        //onEnemyDie();
    }

    public void onLevelReadyEvent()
    {
        onLevelReady();
    }

    public void onPlayerReadyEvent()
    {
        onPlayerReady();
    }
}
