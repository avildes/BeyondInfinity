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

    public delegate void ObjectCollectedHandler(ObjectType type);
    public static event ObjectCollectedHandler onObjectCollected;

    public delegate void ObjectDestoyedHandler(ObjectType type, bool timeout);
    public static event ObjectDestoyedHandler onObjectDestroyed;

    public delegate void LevelHandler();
    public static event LevelHandler onLevelReady;

    public delegate void PlayerHandler();
    public static event PlayerHandler onPlayerReady;

    public delegate void PlayerDeathHandler();
    public static event PlayerDeathHandler onPlayerDied;
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

    public void onObjectCollectedEvent(ObjectType type)
    {
        onObjectCollected(type);
    }

    public void onObjectDestroyedEvent(ObjectType type, bool timeout)
    {
        onObjectDestroyed(type, timeout);
    }

    public void onLevelReadyEvent()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().movePlayer();
        Debug.Log(onLevelReady);
        //onLevelReady();

    }

    public void onPlayerReadyEvent()
    {
        onPlayerReady();
    }

    public void onPlayerDiedEvent()
    {
        onPlayerDied();
    }
}
