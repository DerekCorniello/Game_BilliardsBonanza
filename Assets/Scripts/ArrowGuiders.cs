using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;

public class ArrowGuiders : MonoBehaviour
{
    public CueBallBehavior CueBallBehavior;
    public GameObject CueBall;
    public bool enterPressed = false;
    public Collider2D colliderCue;
    public bool resetSpot = false;

    void Start()
    {
        colliderCue = CueBall.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!CueBallBehavior.gameOver)
        {
            if (Time.timeScale == 1f)
            {
                if (CueBallBehavior.scratched)
                {
                    if (!resetSpot)
                    {
                        if (CueBallBehavior.settleBallsWaitDone == true)
                        {
                            if (Input.GetMouseButton(0))
                            {
                                Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                                Vector3 newPos2 = new Vector3(newPos.x, newPos.y, -1f);
                                CueBall.transform.position = new Vector3(newPos2.x, newPos2.y, -1f);
                                transform.position = new Vector3(newPos2.x, newPos2.y, -1f);

                                colliderCue.enabled = false;
                                StartCoroutine(waitForCueSet());
                            }

                            if (Input.GetMouseButtonUp(0))
                            {
                                if (CueBallBehavior.ifCollidedAfterDrop)
                                {
                                    CueBall.transform.position = new Vector2(-6.8f, -1);
                                    gameObject.transform.position = new Vector3(CueBall.transform.position.x, CueBall.transform.position.y, -1f);
                                    resetSpot = false;
                                    CueBallBehavior.ifCollidedAfterDrop = false;
                                    CueBallBehavior.scratched = true;
                                    CueBallBehavior.TextChanger("You dropped the ball somewhere it can't be!", false);
                                }
                                else
                                {
                                    StartCoroutine(waitForCueSet());
                                }
                            }
                        }
                    }

                    else if (CueBallBehavior.ifCollidedAfterDrop)
                    {
                        CueBall.transform.position = new Vector2(-6.8f, -1);
                        gameObject.transform.position = new Vector3(CueBall.transform.position.x, CueBall.transform.position.y, -1f);
                    }
                }
                else
                {
                    transform.position = new Vector3(-10f, -10f, -.1f);
                    colliderCue.enabled = true;
                }
            }
        }
    }

    IEnumerator waitForCueSet()
    {
        while (!enterPressed)
        {
            if (Input.GetKey("space"))
            {
                enterPressed = true;
            }
            yield return null;
        }

        if (CueBallBehavior.ifCollidedAfterDrop == true)
        {
            colliderCue.enabled = false;
        }

        CueBallBehavior.scratched = false;
        colliderCue.enabled = true;
        enterPressed = false;
    }
}
