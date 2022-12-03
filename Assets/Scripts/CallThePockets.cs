using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CallThePockets : MonoBehaviour
{
    public CueBallBehavior CueBallBehavior;
    public EightBallManager EightBallManager;
    public GameObject coloredPocket;
    public GameObject[] pocketers;
    public GameObject TopRightPocket;
    public GameObject TopSidePocket;
    public GameObject TopLeftPocket;
    public GameObject BottomRightPocket;
    public GameObject BottomSidePocket;
    public GameObject BottomLeftPocket;
    public static bool pocketsAreOut = false;
    
    void Start()
    {
        pocketsAreOut = true;
    }

    void OnMouseDown()
    {
        callIt();
    }

    void callIt()
    {
        StartCoroutine(wait());
        if (EightBallManager.pocketSelected == null)
        {
            EightBallManager.pocketSelected = gameObject.name;

            pocketers = GameObject.FindGameObjectsWithTag("pocketer");
            foreach (GameObject pocketer in pocketers)
            {
                if (pocketer != gameObject)
                {
                    pocketer.SetActive(false);
                }
            }

            if (EightBallManager.pocketSelected == "Pocket_Caller_0")
            {
                EightBallManager.pocketSelected = TopRightPocket.name;
            }
            else if (EightBallManager.pocketSelected == "Pocket_Caller_1")
            {
                EightBallManager.pocketSelected = TopSidePocket.name;
            }
            else if (EightBallManager.pocketSelected == "Pocket_Caller_2")
            {
                EightBallManager.pocketSelected = TopLeftPocket.name;
            }
            else if (EightBallManager.pocketSelected == "Pocket_Caller_3")
            {
                EightBallManager.pocketSelected = BottomRightPocket.name;
            }
            else if (EightBallManager.pocketSelected == "Pocket_Caller_4")
            {
                EightBallManager.pocketSelected = BottomSidePocket.name;
            }
            else if (EightBallManager.pocketSelected == "Pocket_Caller_5")
            {
                EightBallManager.pocketSelected = BottomLeftPocket.name;
            }

            coloredPocket.transform.position = transform.position;
            coloredPocket.SetActive(true);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.1f);
        EightBallManager.selectingAPocket = false;
        gameObject.transform.position = new Vector2(0f, 8f);
    }
}
