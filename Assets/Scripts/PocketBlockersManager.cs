using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketBlockersManager : MonoBehaviour
{
    public GameObject pocket0;
    public GameObject pocket1;
    public GameObject pocket2;
    public GameObject pocket3;
    public GameObject pocket4;
    public GameObject pocket5;
    public static GameObject[] pockets;

    void Start()
    {
        pockets = new GameObject[] {pocket0, pocket1, pocket2, pocket3, pocket4, pocket5};
        removePockets();
    }

    public static void placePockets()
    {
        int randInt1 = UnityEngine.Random.Range(0, 8);
        int randInt2 = UnityEngine.Random.Range(0, 10);
        int randInt3 = UnityEngine.Random.Range(0, 12);
        int randInt4 = UnityEngine.Random.Range(0, 14);

        if (randInt1 <= 5)
        {
            pockets[randInt1].SetActive(true);
        }
        if (randInt2 <= 5)
        {
            pockets[randInt2].SetActive(true);
        }
        if (randInt3 <= 5)
        {
            pockets[randInt3].SetActive(true);
        }
        if (randInt4 <= 5)
        {
            pockets[randInt4].SetActive(true);
        }
    }

    public static void removePockets()
    {
        foreach (GameObject pocket in pockets)
        {
            pocket.SetActive(false);
        }
    }
}
