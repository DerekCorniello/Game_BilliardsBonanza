using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pocketing : MonoBehaviour
{
    public RackEm RackEm;
    public static int redPotted = 0;
    public static int bluePotted = 0;
    public CueBallBehavior CueBallBehavior;
    public static bool ballIn = false;
    public static bool colorDeclared = false;
    public GameObject CueBall;
    private Rigidbody2D rb;
    public EightBallManager EightBallManager;
    public Collider2D[] colliders;
    public static int bluePocketOnHit = 0;
    public static int redPocketOnHit = 0;
    public static bool colorNeedDecl = true;
    public static bool eightPocketed = false;
    public AudioManagerScript aud;
    public CameraShake cam;

    void Start()
    {
        rb = CueBall.GetComponent<Rigidbody2D>() as Rigidbody2D;
        colliders = CueBall.GetComponents<Collider2D>();
        redPotted = 0;
        bluePotted = 0;
        bluePocketOnHit = 0;
        redPocketOnHit = 0;
        ballIn = false;
        colorDeclared = false;
        colorNeedDecl = true;
        eightPocketed = false;
    }

    public void OnCol(GameObject collision, GameObject targetedPocket)
    {
        float magn = collision.GetComponent<Rigidbody2D>().velocity.magnitude;

        StartCoroutine(cam.shake(.15f, .15f, magn));
        int randInt = Random.Range(1, 4);
        string randString = "pocket" + randInt.ToString();
        aud.Play(randString);
        
        ballIn = false;
        if (collision.tag == "redball")
        {
            redPocketOnHit++;
            if (!RackEm.isBreak)
            {
                if (!colorDeclared)
                {
                    colorNeedDecl = true;
                }
            }

            //redPotted = redPotted + 1;

            if (RackEm.turn == 1 && (CueBallBehavior.p1color == "red" || CueBallBehavior.p1color == "N/A"))
            {
                ballIn = true;
            }
            else if (RackEm.turn == 2 && CueBallBehavior.p2color == "red" || CueBallBehavior.p2color == "N/A")
            {
                ballIn = true;
            }
            else if (RackEm.turn == 1 && CueBallBehavior.p1color == "blue")
            {
                CueBallBehavior.TextChanger("Scratch! You pocketed the wrong ball!", false);
                StartCoroutine(scratchedWaitTimer());
            }
            else if (RackEm.turn == 2 && CueBallBehavior.p2color == "blue")
            {
                CueBallBehavior.TextChanger("Scratch! You pocketed the wrong ball!", false);
                StartCoroutine(scratchedWaitTimer());
            }
        }

        else if (collision.tag == "blueball")
        {
            bluePocketOnHit++;
            if (!RackEm.isBreak)
            {
                if (!colorDeclared)
                {
                    colorNeedDecl = true;
                }
            }


            //bluePotted = bluePotted + 1;

            if (RackEm.turn == 1 && (CueBallBehavior.p1color == "blue" || CueBallBehavior.p1color == "N/A"))
            {
                ballIn = true;
            }
            else if (RackEm.turn == 2 && (CueBallBehavior.p2color == "blue" || CueBallBehavior.p2color == "N/A"))
            {
                ballIn = true;
            }
            else if (RackEm.turn == 1 && CueBallBehavior.p1color == "red")
            {
                CueBallBehavior.TextChanger("Scratch! You pocketed the wrong ball!", false);
                StartCoroutine(scratchedWaitTimer());
            }
            else if (RackEm.turn == 2 && CueBallBehavior.p2color == "red")
            {
                CueBallBehavior.TextChanger("Scratch! You pocketed the wrong ball!", false);
                StartCoroutine(scratchedWaitTimer());
            }
        }

        else if (collision.tag == "cue")
        {
            CueBallBehavior.scratched = true;
            CueBallBehavior.TextChanger("You potted the cue!", false);
            collision.gameObject.transform.position = new Vector3(15f, 15f, 0f);
            if (RackEm.eightShot1 && RackEm.turn == 1)
            {
                CueBallBehavior.TextChangerWL("Loser!", 2);
            }
            else if (RackEm.eightShot2 && RackEm.turn == 2)
            {
                CueBallBehavior.TextChangerWL("Loser!", 2);
            }
            foreach (Collider2D colliders2D in colliders)
            {
                colliders2D.enabled = false;
            }
        }

        else if (collision.tag == "eight")
        {
            if (RackEm.eightShot1 == true && RackEm.turn == 1)
            {
                if (EightBallManager.pocketSelected == targetedPocket.name)
                {
                    eightPocketed = true;
                    CueBallBehavior.TextChangerWL("You Win!", 1);
                }
                else
                {
                    CueBallBehavior.TextChangerWL("You Lose!", 2);
                }
            }
            else if (RackEm.eightShot2 == true && RackEm.turn == 2)
            {
                if (EightBallManager.pocketSelected == targetedPocket.name)
                {
                    eightPocketed = true;
                    CueBallBehavior.TextChangerWL("You Win!", 1);
                }
                else
                {
                    CueBallBehavior.TextChangerWL("You Lose!", 2);
                }
            }
            else if (RackEm.isBreak)
            {
                CueBallBehavior.TextChangerWL("Win!", 1);
            }
            else
            {
                CueBallBehavior.TextChangerWL("Game Over!", 2);
            }
        }
    }

    IEnumerator scratchedWaitTimer()
    {
        while (CueBallBehavior.settleBallsWaitDone == false)
        {
            yield return null;
        }
        CueBallBehavior.scratched = true;
    }
}