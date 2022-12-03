using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spriteController : MonoBehaviour
{
    public CueBallBehavior CueBallBehavior;
    public Pocketing Pocketing;
    public EightBallManager EightBallManager;

    public Sprite pb1;
    public Sprite pb2;
    public Sprite pb3;
    public Sprite pb4;
    public Sprite pb5;
    public Sprite pb6;
    public Sprite pb7;
    public Sprite pb8;
    public Sprite pb9;
    public Sprite pb10;
    public Sprite pb11;
    public Sprite pb12;
    public Sprite pb13;
    public Sprite pb14;
    public Sprite pb15;

    public Sprite rb0;
    public Sprite rb1;
    public Sprite rb2;
    public Sprite rb3;
    public Sprite rb4;
    public Sprite rb5;
    public Sprite rb6;
    public Sprite rb7;

    public Sprite bb0;
    public Sprite bb1;
    public Sprite bb2;
    public Sprite bb3;
    public Sprite bb4;
    public Sprite bb5;
    public Sprite bb6;
    public Sprite bb7;

    public GameObject pb;
    public GameObject rbpp;
    public GameObject bbpp;
    public GameObject p1;
    public GameObject p2;
    private Image p1i;
    private Image p2i;
    private Image pbI;
    private Image rbppI;
    private Image bbppI;

    private bool ballsPlaces = false;
    private bool isPaused = false;

    public GameObject PauseMenu;

    private Vector3 p1placer;
    private Vector3 p2placer;

    void Start()
    {
        pbI = pb.GetComponent<Image>();
        rbppI = rbpp.GetComponent<Image>();
        bbppI = bbpp.GetComponent<Image>();
        p1i = p1.GetComponent<Image>();
        p2i = p2.GetComponent<Image>();
        pbI.sprite = pb10;
        p1placer = rbpp.transform.position;
        p2placer = bbpp.transform.position;

        rbpp.SetActive(false);
        bbpp.SetActive(false);
    }

    public void pause()
    {
        if (!CueBallBehavior.gameOver)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f;
                PauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                PauseMenu.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Pocketing.colorDeclared)
        {
            StartCoroutine(growFunction());
        }
        else
        {
            StartCoroutine(waitWhile());
        }

        if (!CueBallBehavior.scratched)
        {
            if (EightBallManager.selectingAPocket == false)
            {
                if (!CueBallBehavior.cueHit)
                {
                    if(CueBallBehavior.forceOnHit == 1)
                    {
                        pbI.sprite = pb1;
                    }
                    else if(CueBallBehavior.forceOnHit == 2)
                    {
                        pbI.sprite = pb2;
                    }
                    else if(CueBallBehavior.forceOnHit == 3)
                    {
                        pbI.sprite = pb3;
                    }
                    else if(CueBallBehavior.forceOnHit == 4)
                    {
                        pbI.sprite = pb4;
                    }
                    else if(CueBallBehavior.forceOnHit == 5)
                    {
                        pbI.sprite = pb5;
                    }
                    else if(CueBallBehavior.forceOnHit == 6)
                    {
                        pbI.sprite = pb6;
                    }
                    else if(CueBallBehavior.forceOnHit == 7)
                    {
                        pbI.sprite = pb7;
                    }
                    else if(CueBallBehavior.forceOnHit == 8)
                    {
                        pbI.sprite = pb8;
                    }
                    else if(CueBallBehavior.forceOnHit == 9)
                    {
                        pbI.sprite = pb9;
                    }
                    else if(CueBallBehavior.forceOnHit == 10)
                    {
                        pbI.sprite = pb10;
                    }
                    else if(CueBallBehavior.forceOnHit == 11)
                    {
                        pbI.sprite = pb11;
                    }
                    else if(CueBallBehavior.forceOnHit == 12)
                    {
                        pbI.sprite = pb12;
                    }
                    else if(CueBallBehavior.forceOnHit == 13)
                    {
                        pbI.sprite = pb13;
                    }
                    else if(CueBallBehavior.forceOnHit == 14)
                    {
                        pbI.sprite = pb14;
                    }
                    else if(CueBallBehavior.forceOnHit == 15)
                    {
                        pbI.sprite = pb15;
                    }    
                }
            }
        }
    }
    
    IEnumerator waitWhile()
    {
        rbpp.SetActive(false);
        bbpp.SetActive(false);

        while(!Pocketing.colorDeclared)
        {
            yield return null;
        }

        if (CueBallBehavior.p1color == "blue")
        {
            rbpp.transform.position = p2placer;
            bbpp.transform.position = p1placer;
        }
        else
        {
            rbpp.transform.position = p1placer;
            bbpp.transform.position = p2placer;
        }

        rbpp.SetActive(true);
        bbpp.SetActive(true);
    }

    IEnumerator growFunction()
    {
        if (!ballsPlaces)
        {
            ballsPlaces = true;
            rbpp.transform.localScale = new Vector3(0.015625f, 0.015625f, 0.015625f);
            bbpp.transform.localScale = new Vector3(0.015625f, 0.015625f, 0.015625f);

            for (int i = 0; i < 12; i++)
            {
                rbpp.transform.localScale = rbpp.transform.localScale * 1.5f;
                bbpp.transform.localScale = bbpp.transform.localScale * 1.5f;

                yield return new WaitForSeconds(.01f);
            }
        }

        if (Pocketing.redPotted == 0)
        {
            rbppI.sprite = rb0;
        }
        else if (Pocketing.redPotted == 1)
        {
            rbppI.sprite = rb1;
        }
        else if (Pocketing.redPotted == 2)
        {
            rbppI.sprite = rb2;
        }
        else if (Pocketing.redPotted == 3)
        {
            rbppI.sprite = rb3;
        }
        else if (Pocketing.redPotted == 4)
        {
            rbppI.sprite = rb4;
        }
        else if (Pocketing.redPotted == 5)
        {
            rbppI.sprite = rb5;
        }
        else if (Pocketing.redPotted == 6)
        {
            rbppI.sprite = rb6;
        }
        else if (Pocketing.redPotted == 7)
        {
            rbppI.sprite = rb7;
        }

        if (Pocketing.bluePotted == 0)
        {
            bbppI.sprite = bb0;
        }
        else if (Pocketing.bluePotted == 1)
        {
            bbppI.sprite = bb1;
        }
        else if (Pocketing.bluePotted == 2)
        {
            bbppI.sprite = bb2;
        }
        else if (Pocketing.bluePotted == 3)
        {
            bbppI.sprite = bb3;
        }
        else if (Pocketing.bluePotted == 4)
        {
            bbppI.sprite = bb4;
        }
        else if (Pocketing.bluePotted == 5)
        {
            bbppI.sprite = bb5;
        }
        else if (Pocketing.bluePotted == 6)
        {
            bbppI.sprite = bb6;
        }
        else if (Pocketing.bluePotted == 7)
        {
            bbppI.sprite = bb7;
        }
    }
}
