using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RackEm : MonoBehaviour
{
    public GameObject red_Ball;
    public GameObject blue_Ball;
    public GameObject CueBall;
    public GameObject eight;
    public GameObject darkness;
    public GameObject emptyCircle;
    public GameObject arrows;
    public GameObject mask1;
    public GameObject mask2;
    public GameObject mask3;
    public GameObject mask4;
    public GameObject mask5;
    public GameObject mask6;
    public GameObject mask7;
    public GameObject pockguard0;
    public GameObject pockguard1;
    public GameObject pockguard2;
    public GameObject pockguard3;
    public GameObject pockguard4;
    public GameObject pockguard5;
    public CueBallBehavior CueBallBehavior;
    public static bool isBreak = true;
    public static bool eightShot1 = false; 
    public static bool eightShot2 = false; 
    public static int turn = 1;

    public void darknessActivator()
    {
        CueBall.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        eight.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        emptyCircle.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        arrows.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        darkness.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        darkness.SetActive(true);

        GameObject[] pocketsGuarders = GameObject.FindGameObjectsWithTag("blocker");
        GameObject[] blueballsArray = GameObject.FindGameObjectsWithTag("blueball");
        GameObject[] redballsArray = GameObject.FindGameObjectsWithTag("redball");

        foreach(GameObject bb in blueballsArray)
        {
            bb.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        foreach(GameObject rb in redballsArray)
        {
            rb.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        foreach (GameObject b in pocketsGuarders)
        {
            b.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        mask1.SetActive(true);
        mask2.SetActive(true);
        mask3.SetActive(true);
        mask4.SetActive(true);
        mask5.SetActive(true);
        mask6.SetActive(true);
        mask7.SetActive(true);
    }

    public void darknessDeactivator()
    {
        CueBall.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        eight.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        emptyCircle.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        arrows.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        darkness.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        darkness.SetActive(false);

        GameObject[] blueballsArray = GameObject.FindGameObjectsWithTag("blueball");
        GameObject[] redballsArray = GameObject.FindGameObjectsWithTag("redball");
        GameObject[] pocketsGuarders = GameObject.FindGameObjectsWithTag("blocker");

        foreach (GameObject bb in blueballsArray)
        {
            bb.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
        foreach (GameObject rb in redballsArray)
        {
            rb.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
        foreach (GameObject b in pocketsGuarders)
        {
            b.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }

        mask1.SetActive(false);
        mask2.SetActive(false);
        mask3.SetActive(false);
        mask4.SetActive(false);
        mask5.SetActive(false);
        mask6.SetActive(false);
        mask7.SetActive(false);
    }

    void Start()
    {
        turn = 1;
        eightShot1 = false;
        eightShot2 = false;
        isBreak = true;

        float[] xb = new float[] { 4.6f, 5.4f, 5.4f, 6.2f, 6.2f, 7f, 7f };
        float[] yb = new float[]  { -1.75f, -2.25f, -.25f, -1.75f, .25f, -2.25f, -.25f };
        float[] xr = new float[] { 3.8f, 4.6f, 6.2f, 6.2f, 7f, 7f, 7f };
        float[] yr = new float[] { -1.25f, -.75f, -2.75f, -.75f, -3.25f, -1.25f, .75f };

        CueBall.transform.position = new Vector3(-6.8f, -1f, 0);
        eight.transform.position = new Vector3(5.4f, -1.25f, 0);
        
        for (int i = 0; i < 7; i++)
        {
            GameObject blueBall = Instantiate(blue_Ball);
            GameObject redBall = Instantiate(red_Ball);
            blueBall.name = "Blue_Ball_" + (i + 1);
            redBall.name = "Red_Ball_" + (i + 1);
            blueBall.transform.position = new Vector3(xb[i], yb[i], 0);
            redBall.transform.position = new Vector3(xr[i], yr[i], 0);

            if (TitleScreenMenus.ruleset[1] == true)
            {
                blueBall.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                redBall.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            else
            {
                blueBall.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                redBall.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
        }

        Destroy(red_Ball);
        Destroy(blue_Ball);

        if (TitleScreenMenus.ruleset[1] == true)
        {
            darknessActivator();
        }
        else
        {
            darknessDeactivator();
        }

    }
}
