using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BallSwitcherScript : MonoBehaviour
{
    public static List<Vector2> redballspos = new List<Vector2>();
    public static List<Vector2> blueballspos = new List<Vector2>();
    public static List<GameObject> redballsObj = new List<GameObject>();
    public static List<GameObject> blueballsObj = new List<GameObject>();
    public static List<GameObject> transferList = new List<GameObject>();

    public static void redballadd(Vector2 pos, GameObject obj)
    {
        if (!redballsObj.Contains(obj))
        {
            redballspos.Add(pos);
            redballsObj.Add(obj);
        }
    }
    public static void blueballadd(Vector2 pos, GameObject obj)
    {
        if (!blueballsObj.Contains(obj))
        {
            blueballspos.Add(pos);
            blueballsObj.Add(obj);
        }
    }
    public static void resetLists()
    {
        redballspos = new List<Vector2>();
        blueballspos = new List<Vector2>();
        redballsObj = new List<GameObject>();
        blueballsObj = new List<GameObject>();
    }
    public static List<GameObject> switchEm()
    {
        transferList = new List<GameObject>();
        int min = 0;

        if (redballspos.Count > blueballspos.Count)
        {
            min = blueballspos.Count;
        }
        else if (redballspos.Count < blueballspos.Count)
        {
            min = redballspos.Count;
        }
        else
        {
            min = blueballspos.Count;
        }

        int maxNumSwitches = min / 2;

        if (maxNumSwitches < 1)
        {
            if (redballspos.Count != 0 && blueballspos.Count != 0)
            {
                maxNumSwitches = 1;
            }
        }

        List<int> redballsleft = new List<int>();
        List<int> blueballsleft = new List<int>();

        for (int i = 0; i < redballspos.Count; i++) // Creates a list of the indexes of available in the List
        {
            redballsleft.Add(i);
        }
        for (int i = 0; i < blueballspos.Count; i++)
        {
            blueballsleft.Add(i);
        }

        for (int i = redballsleft.Count; i > maxNumSwitches; i--)
        {
            int x = Random.Range(0, redballsleft.Count);
            redballsleft.Remove(x);
        }

        for (int i = blueballsleft.Count; i > maxNumSwitches; i--)
        {
            int x = Random.Range(0, blueballsleft.Count);
            blueballsleft.Remove(x);
        }

        for (int i = 0; i < maxNumSwitches; i++)
        {
            int redRandInd = Random.Range(0, redballsleft.Count);
            int blueRandInd = Random.Range(0, blueballsleft.Count); // Of the remaining balls, we will generate a random index

            redballsObj[redballsleft[redRandInd]].transform.position = new Vector2(blueballspos[blueballsleft[blueRandInd]].x, blueballspos[blueballsleft[blueRandInd]].y); //Using the index, we will switch the position of the balls
            blueballsObj[blueballsleft[blueRandInd]].transform.position = new Vector2(redballspos[redballsleft[redRandInd]].x, redballspos[redballsleft[redRandInd]].y);

            transferList.Add(redballsObj[redballsleft[redRandInd]]);
            transferList.Add(blueballsObj[blueballsleft[blueRandInd]]);

            redballsleft.Remove(redballsleft[redRandInd]);
            blueballsleft.Remove(blueballsleft[blueRandInd]); // Then remove the possiblity of reswitching them by accident.
        }

        if (transferList.Count == 0)
        {
            return null;
        }
        else
        {
            return transferList;
        }
    }
}