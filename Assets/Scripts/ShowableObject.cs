using UnityEngine;
using System.Collections;

public class ShowableObject : MonoBehaviour
{
    private float appearanceSpeed;

    public int quantityOfAppearances;

    public float intervalBetweenAppearances;

    private bool rotate;

    private float smooth = 10f;

    private Vector3 showTarget = new Vector3(0, 0, 180);

    private Vector3 hideTarget = new Vector3(0, 0, 0);

    private Vector3 targetAngles;

    private int appearanceCounter;

    public void SetAppearanceSpeed(float value)
    {
        appearanceSpeed = value;
    }

    void Update()
    {
        if(rotate)
        {
            transform.parent.eulerAngles = Vector3.Lerp(transform.parent.eulerAngles, targetAngles, smooth * Time.deltaTime);

            if (transform.parent.eulerAngles.Equals(targetAngles)) rotate = false;
        }
    }

    void Start()
    {
        rotate = false;

        EventManager.onPlayerReady += ShowObject;
        EventManager.onPlayerDied += PlayerDied;

        appearanceCounter = 0;
    }

    void OnDestroy()
    {
        transform.parent.rotation = Quaternion.identity;

        EventManager.onPlayerReady -= ShowObject;
        EventManager.onPlayerDied -= PlayerDied;
    }

    void PlayerDied()
    {
        StopAllCoroutines();

        Destroy(gameObject);
    }

    void ShowObject()
    {
        if(appearanceCounter == quantityOfAppearances)
        {
            ObjectType type = ObjectType.ORE;

            if(gameObject.GetComponent<Enemy>())
            {
                type = ObjectType.ENEMY;
            }

            EventManager.Instance.onObjectDestroyedEvent(type, true);

            Destroy(gameObject);
        }
        
        appearanceCounter++;

        //Rotate
        /*
        Quaternion quat = transform.parent.rotation;
        quat.z = 180;
        transform.parent.rotation = quat;
        */
        targetAngles = showTarget;

        rotate = true;

        StartCoroutine(WaitToShoot());
    }

    void HideObject()
    {
        //Rotate
        /*Quaternion quat = transform.parent.rotation;
        quat.z = 0;
        transform.parent.rotation = quat;
        */
        targetAngles = hideTarget;

        rotate = true;

    }

    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(appearanceSpeed);

        if (gameObject.GetComponent<Enemy>())
        {
            gameObject.GetComponent<Enemy>().Shoot();
        }

        HideObject();

        yield return new WaitForSeconds(intervalBetweenAppearances);

        ShowObject();
    }
}
