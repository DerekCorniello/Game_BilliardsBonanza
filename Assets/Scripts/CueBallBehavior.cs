using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CueBallBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public Camera cam;
    public float forceOnHit = 10;
    public RackEm RackEm;
    public bool cueHit = false;
    public Pocketing Pocketing;
    public RackEm rackEm;
    public GameObject eight;
    public string p1color = "N/A";
    public string p2color = "N/A";
    public static bool isFirstContact = true;
    public EightBallManager EightBallManager;
    public static bool scratched = false;
    public GameObject arrows;
    public bool ifCollidedAfterDrop = false;
    public ArrowGuiders ArrowGuiders;
    public bool settleBallsWaitDone = false;
    public GameObject SightAimer;
    public GameObject forceHelper;
    public CollisionDetection CollisionDetection;
    public CallThePockets CallThePockets;
    public bool ballHit = false;
    public Animator Anim;
    public bool canBallsMove = false;
    public LayerMask layermask;
    public GameObject RedStick;
    public GameObject BlueStick;
    public AudioManagerScript aud;
    public GameObject PowerBar;
    public GameObject Arrow;
    public Image pb;
    public GameObject MessageBox;
    public Text messageText;
    public Text scratchText;
    public GameObject MessageBoxWL;
    public GameObject pauseMenu;
    public Text messageTextWL;
    public Animator fader;
    public CameraShake camshake;
    public ParticleSystem particle;
    public ParticleSystem popEff;
    public int CueParticle = 0;
    public bool wallScratch = false;
    public transitions transitions;
    public GameObject poolTable;
    public Bounds tableBounds;
    public bool collectPos = false;
    public bool earthquakeActive = false;
    public bool gameOver = false;
    public SightAimer sa;
    public bool timerOver = false;

    void Start()
    {
        tableBounds = poolTable.GetComponent<SpriteRenderer>().sprite.bounds;
        Vector3 tableBoundsVector = Camera.main.WorldToScreenPoint(tableBounds.min);
        Vector3 tableBoundsVector2 = Camera.main.WorldToScreenPoint(tableBounds.max);
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        Anim = GetComponentInChildren<Animator>();
        Arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        isFirstContact = true;
        scratched = false;
        gameOver = false;
        aud.Play("Track1");
        BallSwitcherScript.resetLists();
    }

    IEnumerator particleFunct(ParticleSystem currentParticle)
    {
        currentParticle.Play();
        yield return new WaitForSeconds(1f);
        Destroy(currentParticle);
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Time.timeScale == 1f)
            {
                if (scratched == false)
                {
                    if (EightBallManager.selectingAPocket == false)
                    {
                        if (!cueHit)
                        {
                            if (!transitions.exitPressed)
                            {
                                if (MouseBounds.mouseInBounds)
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        if (TitleScreenMenus.ruleset[0] == true)
                                        {
                                            List<GameObject> partTransforms = BallSwitcherScript.switchEm();
                                            
                                            if (partTransforms != null)
                                            {
                                                int namenum = 0;
                                                foreach (GameObject obj in partTransforms)
                                                {
                                                    ParticleSystem pop = Instantiate(popEff);
                                                    pop.name = "pop_" + namenum.ToString();
                                                    pop.transform.position = partTransforms[namenum].transform.position;
                                                    pop.Play();
                                                    namenum++;
                                                }
                                            }
                                        }
                                        if (TitleScreenMenus.ruleset[2] == true)
                                        {
                                            PocketBlockersManager.placePockets();
                                        }
                                        if (TitleScreenMenus.ruleset[1] == true)
                                        {
                                            RackEm.darknessDeactivator();
                                        }
                                        StartCoroutine(afterStrike());
                                        BallSwitcherScript.resetLists();
                                        collectPos = false;
                                    }
                                }

                                if (Input.GetKeyDown("right"))
                                {
                                    forceOnHit = forceOnHit + 1;

                                    if (forceOnHit > 15)
                                    {
                                        forceOnHit = 15;
                                    }
                                    else
                                    {
                                        RedStick.transform.position -= RedStick.transform.up * .2f;
                                        BlueStick.transform.position -= BlueStick.transform.up * .2f;
                                    }
                                }
                                if (Input.GetKeyDown("left"))
                                {
                                    forceOnHit = forceOnHit - 1;

                                    if (forceOnHit <= 0)
                                    {
                                        forceOnHit = 1;
                                    }
                                    else
                                    {
                                        RedStick.transform.position += RedStick.transform.up * .2f;
                                        BlueStick.transform.position += BlueStick.transform.up * .2f;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void TextChanger(string message, bool displayScratchedConfirm)
    {
        if (!gameOver)
        {
            messageText.text = message;
            MessageBox.SetActive(true);
            if (displayScratchedConfirm)
            {
                scratchText.gameObject.SetActive(true);
            }
            else
            {
                scratchText.gameObject.SetActive(false);
            }
            StartCoroutine(FadeAway());
        }
    }

    public void TextChangerWL(string message, int winning)
    {
        gameOver = true;
        string winmess = null;
        if (RackEm.turn == 1 && winning == 1)
        {
            winmess = "Player 1 Wins" + "\n" + "Player 2 Loses";
        }
        else if (RackEm.turn == 1 && winning == 2)
        {
            winmess = "Player 2 Wins" + "\n" + "Player 1 Loses";
        }
        else if (RackEm.turn == 2 && winning == 1)
        {
            winmess = "Player 2 Wins" + "\n" + "Player 1 Loses";
        }
        else
        {
            winmess = "Player 1 Wins" + "\n" + "Player 2 Loses";
        }
        string ggmess = message + "\n" + winmess;
        
        StartCoroutine(ggs(ggmess));
    }

    IEnumerator ggs(string mess)
    {
        while (!settleBallsWaitDone)
        {
            yield return null;
        }

        if (CallThePockets.pocketsAreOut)
        {
            CallThePockets.coloredPocket.SetActive(false);
        }

        messageTextWL.text = mess;
        MessageBoxWL.SetActive(true);

        bool esc_click = false;
        while (esc_click == false)
        {
            if (Input.GetKeyDown("escape") == true)
            {
                esc_click = true;
            }
            yield return new WaitForSeconds(.01f);
        }
        pauseMenu.SetActive(false);
        MessageBox.SetActive(false);
        MessageBoxWL.SetActive(false);
        transitions.exitToMenu();
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(5f);
        fader.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        MessageBox.SetActive(false);

        Color txt = messageText.color;
        txt.a = 1;
        messageText.color = txt;
        scratchText.color = txt;

        fader.SetBool("Fade", false);
    }

    IEnumerator afterStrike()
    {
        Vector2 veloVect = (forceHelper.transform.position - transform.position) * forceOnHit;

        for(int i = 0; i < 100; i++)
        {
            RedStick.transform.position += RedStick.transform.up * forceOnHit * .0075f;
            BlueStick.transform.position += BlueStick.transform.up * forceOnHit * .0075f;
            if (RedStick.transform.localPosition.x >= -5.25f || BlueStick.transform.localPosition.x >= -5.25f)
            {
                RedStick.transform.position += RedStick.transform.up * .05f;
                BlueStick.transform.position += BlueStick.transform.up * .05f;
                break;
            }
            yield return new WaitForSeconds(.0001f);
        }

        int randInt = Random.Range(1, 6);
                                        string randString = "cue" + randInt.ToString();
                                        aud.Play(randString);

        StartCoroutine(camshake.shake(.15f, .4f, rb.velocity.magnitude));
        Vector2 EffectSpawn = transform.position;
        ParticleSystem hitParticle = Instantiate(particle);

        ParticleSystem.MainModule ps = hitParticle.main;
        float magn = veloVect.magnitude / 1.25f;
        ps.maxParticles = Mathf.RoundToInt(magn);

        hitParticle.name = "cue_particle_" + CueParticle.ToString();
        CueParticle++;
        hitParticle.transform.position = EffectSpawn;
        StartCoroutine(particleFunct(hitParticle));
        StartCoroutine(animTimer());
        scratched = false;
        settleBallsWaitDone = false;
        rb.velocity = veloVect;
        StartCoroutine(settleBallsTimer());
        Pocketing.ballIn = false;
        StartCoroutine(killParticle(hitParticle));
    }
    
    IEnumerator killParticle(ParticleSystem particle)
    {
        particle.Play();
        yield return new WaitForSeconds(1f);
        Destroy(particle.gameObject);
    }

    IEnumerator animTimer()
    {
        Anim.SetBool("initialContact", true);
        yield return new WaitForSeconds(.1f);
        Anim.SetBool("initialContact", false);
    } 

    void OnCollisionEnter2D(Collision2D Collision)
    {
        if (!scratched)
        {
            if (Collision.gameObject.tag == "eight")
            {
                ballHit = true;
                if (isFirstContact && Pocketing.colorDeclared)
                {
                    if (RackEm.turn == 1 && RackEm.eightShot1 == true)
                    {
                        
                    }
                    else if (RackEm.turn == 2 && RackEm.eightShot2 == true)
                    {

                    }
                    else 
                    {
                        scratched = true;
                        TextChanger("You hit the eight ball first! Hit your own ball first next time!", false);
                    }
                }
                isFirstContact = false;
            }

            else if (Collision.gameObject.tag == "redball")
            {
                ballHit = true;

                if (isFirstContact && Pocketing.colorDeclared)
                {
                    if (RackEm.turn == 1 && p1color == "blue")
                    {
                        scratched = true;
                        TextChanger("You hit the wrong ball first! Hit your own ball first next time!", false);
                    }
                    else if (RackEm.turn == 2 && p2color == "blue")
                    {
                        scratched = true;
                        TextChanger("You hit the wrong ball first! Hit your own ball first next time!", false);
                    }
                }
                isFirstContact = false;
            }

            else if (Collision.gameObject.tag == "blueball")
            {
                ballHit = true;

                if (isFirstContact && Pocketing.colorDeclared)
                {
                    if (RackEm.turn == 1 && p1color == "red")
                    {
                        scratched = true;
                        TextChanger("You hit the wrong ball first! Hit your own ball first next time!", false);
                    }
                    else if (RackEm.turn == 2 && p2color == "red")
                    {
                        scratched = true;
                        TextChanger("You hit the wrong ball first! Hit your own ball first next time!", false);
                    }
                }
                isFirstContact = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (scratched)
        {
            ifCollidedAfterDrop = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (scratched)
        {
            ifCollidedAfterDrop = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (scratched)
        {
            ifCollidedAfterDrop = false;
        }
    }

    IEnumerator settleBallsTimer()
    {
        timerOver = false;
        canBallsMove = true;
        cueHit = true;
        settleBallsWaitDone = false;
        yield return new WaitForSeconds(7.9f);
        settleBallsWaitDone = true;
        yield return new WaitForSeconds(.1f);

        if (!gameOver)
        {
            if (TitleScreenMenus.ruleset[3] == true)
            {
                earthquakeActive = true;
                yield return new WaitForSeconds(2f);
                earthquakeActive = false;
            }
            if (TitleScreenMenus.ruleset[2] == true)
            {
                PocketBlockersManager.removePockets();
            }
            if (TitleScreenMenus.ruleset[1] == true)
            {
                RackEm.darknessActivator();
            }
        }

        collectPos = true;

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        sa.clicked = false;

        RedStick.transform.localPosition = new Vector2(-8, 0);
        BlueStick.transform.localPosition = new Vector2(-8, 0);
        cueHit = false;
        canBallsMove = false;
        if (CallThePockets.pocketsAreOut)
        {
            CallThePockets.coloredPocket.SetActive(false);
        }

        GameObject[] leftoverPop = GameObject.FindGameObjectsWithTag("collisionParticle");
        GameObject[] leftoverQuake = GameObject.FindGameObjectsWithTag("QuakeParticle");
        GameObject[] leftoverCollision = GameObject.FindGameObjectsWithTag("pop");

        if (leftoverCollision != null)
        {
            foreach (GameObject obj in leftoverCollision)
            {
                Destroy(obj);
            }
        }
        if (leftoverQuake != null)
        {
            foreach (GameObject obj in leftoverQuake)
            {
                Destroy(obj);
            }
        }
        if (leftoverPop != null)
        {
            foreach (GameObject obj in leftoverPop)
            {
                Destroy(obj);
            }
        }

        if (RackEm.isBreak == false) 
        { 
            if (Pocketing.colorNeedDecl == true && Pocketing.colorDeclared == false)
            {
                if (Pocketing.bluePocketOnHit != Pocketing.redPocketOnHit)
                {
                    if (Pocketing.bluePocketOnHit > Pocketing.redPocketOnHit)
                    {
                        if (RackEm.turn == 1)
                        {
                            p1color = "blue";
                            p2color = "red";
                        }
                        if (RackEm.turn == 2)
                        {
                            p1color = "red";
                            p2color = "blue";
                        }
                        Pocketing.colorDeclared = true;
                        Pocketing.colorNeedDecl = false;
                    }
                    else if (Pocketing.bluePocketOnHit < Pocketing.redPocketOnHit)
                    {
                        if (RackEm.turn == 1)
                        {
                            p1color = "red";
                            p2color = "blue";  
                        }
                        if (RackEm.turn == 2)
                        {
                            p1color = "blue";
                            p2color = "red";
                        }
                        Pocketing.colorDeclared = true;
                        Pocketing.colorNeedDecl = false;
                    }
                }
            }
        }

        if (Pocketing.redPotted == 7 && p1color == "red")
        {
            RackEm.eightShot1 = true;
        }
        else if (Pocketing.redPotted == 7 && p2color == "red")
        {
            RackEm.eightShot2 = true;
        }
        if (Pocketing.bluePotted == 7 && p1color == "blue")
        {
            RackEm.eightShot1 = true;
        }
        else if (Pocketing.bluePotted == 7 && p2color == "blue")
        {
            RackEm.eightShot2 = true;
        }

        if (CollisionDetection.walls_Hit <= 2)
        {
            if (ballHit == false)
            {
                if (!scratched)
                {
                    scratched = true;
                    wallScratch = true;
                }
            }
        }

        if (scratched)
        {
            gameObject.transform.position = new Vector2(-6.8f, -1);
            arrows.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            gameObject.SetActive(true);
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (wallScratch)
            {
                TextChanger("You didn't hit enough sides of the table. Your opponent now has ball in hand.", true);
            }
            else
            {
                TextChanger("Your Opponent scratched! Place the ball wherever you'd like on the table!", true);
            }
        }

        if (!Pocketing.ballIn || scratched)
        {
            if (RackEm.turn == 1)
            {
                RackEm.turn = 2;
                StartCoroutine(RotateArrow());
            }
            else
            {
                RackEm.turn = 1;
                StartCoroutine(RotateArrow());
            }
        }
        gameObject.SetActive(true);
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

        foreach (Collider2D colliders2D in Pocketing.colliders)
        {
            colliders2D.enabled = true;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        rb.velocity = new Vector2(0, 0);

        timerOver = true;
        wallScratch = false;
        RackEm.isBreak = false;
        Pocketing.bluePocketOnHit = 0;
        Pocketing.redPocketOnHit = 0;
        EightBallManager.pocketersPlaced = false;
        EightBallManager.pocketSelected = null;
        EightBallManager.selectingAPocket = false;
        forceOnHit = 10;
        ifCollidedAfterDrop = false;
        CollisionDetection.walls_Hit = 0;
        ballHit = false;
        isFirstContact = true;

        GameObject[] reds = GameObject.FindGameObjectsWithTag("redball");
        GameObject[] blues = GameObject.FindGameObjectsWithTag("blueball");

        Pocketing.redPotted = 7 - reds.Length;
        Pocketing.bluePotted = 7 - blues.Length;

        UnityEngine.Debug.Log("Red Balls: " + Pocketing.redPotted);
        UnityEngine.Debug.Log("Blue Balls: " + Pocketing.bluePotted);

    }

    IEnumerator RotateArrow()
    {
        if (!gameOver)
        {
            Arrow.transform.localScale = new Vector3(Arrow.transform.localScale.x, -Arrow.transform.localScale.y, Arrow.transform.localScale.z);
            Vector3 arrowScale = Arrow.transform.localScale;

            for (int i = 0; i < 36; i++)
            {
                Arrow.transform.Rotate(0, 0, 5);

                yield return new WaitForSeconds(Time.deltaTime);
            }
            Arrow.transform.localScale = arrowScale;
        }
    }

    IEnumerator waitingForBreak()
    {
        yield return new WaitForSeconds(7.5f);
        RackEm.isBreak = false;
    }
}