using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightBallManager : MonoBehaviour
{
    public GameObject pocketSelectorSprite;
    public CueBallBehavior CueBallBehavior;
    public static string pocketSelected = null;
    public static bool pocketersPlaced = false;
    public static bool selectingAPocket = false;
    private GameObject[] pocketers;

    void Start()
    {
        pocketers = GameObject.FindGameObjectsWithTag("pocketer");
        selectingAPocket = false;
        pocketersPlaced = false;
        pocketSelected = null;
    }

    void Update()
    {
        if (!pocketersPlaced)
        {
            if (RackEm.eightShot2 == true && RackEm.turn == 2 && CueBallBehavior.settleBallsWaitDone == true)
            {
                foreach (GameObject obj in pocketers)
                {
                    obj.SetActive(true);
                }

                PocketSelectorPlacement();
                pocketersPlaced = true;
                selectingAPocket = true;
            }
            else if (RackEm.eightShot1 == true && RackEm.turn == 1 && CueBallBehavior.settleBallsWaitDone == true)
            {
                foreach (GameObject obj in pocketers)
                {
                    obj.SetActive(true);
                }

                PocketSelectorPlacement();
                pocketersPlaced = true;
                selectingAPocket = true;
            }
            else
            {
                foreach (GameObject obj in pocketers)
                {
                    obj.SetActive(false);
                }
            }
        }
    } 

    void PocketSelectorPlacement()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject pocket = Instantiate(pocketSelectorSprite);
            pocket.name = "Pocket_Caller_" + i;

            if (i == 0)
            {
                pocket.transform.position = new Vector3(10f, 3.5f, -1.5f); //top right
            }
            else if(i == 1)
            {
                pocket.transform.position = new Vector3(0f, 3.5f, -1.5f); //top side
            }
            else if (i == 2)
            {
                pocket.transform.position = new Vector3(-10f, 3.5f, -1.5f); // top left
            }
            else if (i == 3)
            {
                pocket.transform.position = new Vector3(10f, -5.6f, -1.5f); // bottom right
            }
            else if (i == 4)
            {
                pocket.transform.position = new Vector3(0f, -5.6f, -1.5f); // bottom side
            }
            else if (i == 5)
            {
                pocket.transform.position = new Vector3(-10f, -5.6f, -1.5f); // bottom left
            }

            pocket.SetActive(true);
        }
    }
}
