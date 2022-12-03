using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SightAimer : MonoBehaviour
{
    public CueBallBehavior CueBallBehavior;
    public GameObject CueBall;
    private LineRenderer lr;
    public GameObject emptyGuider;
    public LayerMask layerMask;
    private RaycastHit2D[] rays;
    private RaycastHit2D shortestRay;
    public float shortestDistance;
    public GameObject emptyCircle;
    public float ballRadius;
    public RackEm RackEm;
    public EightBallManager EightBallManager;
    public transitions transitions;
    public bool clicked = false;
    public MouseBounds MouseBounds;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.useWorldSpace = true;
        Renderer rend = GetComponent<Renderer>();
        ballRadius = rend.bounds.extents.magnitude / 1.7f;
        clicked = false;
    }

    void Update()
    {
        if (!CueBallBehavior.gameOver)
        {
            if (!transitions.exitPressed)
            {
                if (Time.timeScale == 1f)
                {
                    if (CueBallBehavior.cueHit == false)
                    {
                        if (CueBallBehavior.scratched == false)
                        {
                            if (EightBallManager.selectingAPocket == false)
                            {
                                if (MouseBounds.mouseInBounds)
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        clicked = true;
                                    }
                                }

                                if (!clicked)
                                {
                                    //Movement
                                    Vector3 mouseDirectionVector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                                    Vector3 mouseDirectionVectorFixed = new Vector3(mouseDirectionVector.x, mouseDirectionVector.y, 0);
                                    Vector3 targ = transform.position;
                                    targ.z = 0f;
                                    targ.x = targ.x - mouseDirectionVectorFixed.x;
                                    targ.y = targ.y - mouseDirectionVectorFixed.y;
                                    float angle = (Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg) + 180;
                                    CueBall.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                                }

                                //raycast and LR
                                if (TitleScreenMenus.ruleset[4] == true)
                                {
                                    RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, ballRadius, transform.right, 100, layerMask);

                                    lr.SetPosition(0, transform.position);
                                    lr.SetPosition(1, this.transform.position + this.transform.right * hit.distance);
                                    lr.enabled = true;
                                    Vector2 toContact = hit.point - (Vector2)transform.position;
                                    Vector3 toClosestPointOnRay = Vector3.Project(toContact, transform.right);
                                    float d1 = Vector2.Distance(toClosestPointOnRay, toContact);
                                    Vector3 ballCenter = toClosestPointOnRay - (Mathf.Sqrt(ballRadius * ballRadius - d1 * d1) * toClosestPointOnRay.normalized);
                                    emptyCircle.transform.position = this.transform.position + ballCenter;
                                    emptyCircle.SetActive(true);
                                }
                            }
                            else
                            {
                                lr.enabled = false;
                                emptyCircle.SetActive(false);
                            }
                        }
                        else
                        {
                            lr.enabled = false;
                            emptyCircle.SetActive(false);
                        }
                    }
                    else
                    {
                        lr.enabled = false;
                        emptyCircle.SetActive(false);
                    }
                }
            }
        }
        else
        {
            lr.enabled = false;
            emptyCircle.SetActive(false);
        }
    }
}
