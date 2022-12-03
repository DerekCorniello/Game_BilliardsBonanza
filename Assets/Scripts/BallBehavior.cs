using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    public float smoothRotateSpeed = 1f;
    public CueBallBehavior CueBallBehavior;
    private Collider2D[] colls;
    public Pocketing Pocketing;
    public ParticleSystem particle;
    public ParticleSystem quake;
    static public int particleNum = 0;
    private bool dataHasBeenCollected = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colls = GetComponents<Collider2D>();
        particleNum = 0;
    }

    void Update()
    {
        if (CueBallBehavior.canBallsMove == false)
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up), smoothRotateSpeed * rb.velocity.magnitude * Time.deltaTime);
        }

        if (CueBallBehavior.collectPos == true)
        {
            if (dataHasBeenCollected == false)
            {
                if (gameObject.tag == "blueball")
                {
                    BallSwitcherScript.blueballadd(new Vector2(this.transform.position.x, this.transform.position.y), this.gameObject);
                }
                else if (gameObject.tag == "redball")
                {
                    BallSwitcherScript.redballadd(new Vector2(this.transform.position.x, this.transform.position.y), this.gameObject);
                }
                dataHasBeenCollected = true;
            }
        }

        if(CueBallBehavior.earthquakeActive)
        {
            StartCoroutine(Earthquake());
        }
        else
        {
            dataHasBeenCollected = false;
        }
    }
    
    IEnumerator Earthquake()
    {
        GameObject[] activeQuakes = GameObject.FindGameObjectsWithTag("QuakeParticle");

        if (activeQuakes.Length < 21)
        {
            ParticleSystem particle = Instantiate(quake);
            particle.transform.position = gameObject.transform.position;
            particle.Play();
        }

        int x = UnityEngine.Random.Range(0, 4);
        if (x == 1)
        {
            for (int i = 0; i < 10f; i++)
            {
                rb.AddRelativeForce(new Vector2(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f)));
                yield return new WaitForSeconds(.005f);
            }
        }
    }

    void Pocketed(GameObject obj)
    {
        //if (!CueBallBehavior.scratched)
        {
            if (obj.tag == "pocket")
            {
                if (this.tag != "cue")
                { 
                    Pocketing.OnCol(gameObject, obj);

                    foreach (Collider2D collider in colls)
                    {
                        collider.enabled = false;
                        rb.velocity = new Vector2(0, 0);
                    }
                    StartCoroutine(FallAnimation(obj.transform));
                }
                else if (!CueBallBehavior.timerOver)
                {
                    Pocketing.OnCol(gameObject, obj);

                    foreach (Collider2D collider in colls)
                    {
                        collider.enabled = false;
                        rb.velocity = new Vector2(0, 0);
                    }
                    StartCoroutine(FallAnimation(obj.transform));
                }
            }
            else if (gameObject.tag == "cue")
            {
                CueBallBehavior.ifCollidedAfterDrop = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        Pocketed(col.gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 EffectSpawn = new Vector2 (0,0);

        if (col.gameObject.tag != "wall")
        {
            Vector2 thisball = new Vector2(transform.position.x, transform.position.y);
            Vector2 otherball = new Vector2(col.gameObject.transform.position.x, col.gameObject.transform.position.y);
            Vector2 localPositionEffect = otherball - thisball;
            EffectSpawn = thisball + localPositionEffect;
        }
        else
        {
            EffectSpawn = transform.position;
        }

        ParticleSystem hitParticle = Instantiate(particle);
        ParticleSystem.MainModule ps = hitParticle.main;
        Vector2 veloVect = new Vector2(rb.velocity.x, rb.velocity.y);

        float magn = veloVect.magnitude / 1.25f;
        ps.maxParticles = Mathf.RoundToInt(magn);

        hitParticle.name = "particle_" + particleNum.ToString();
        
        particleNum++;
        hitParticle.transform.position = EffectSpawn;
        StartCoroutine(particleFunct(hitParticle));

        Pocketed(col.gameObject);
    }

    IEnumerator particleFunct (ParticleSystem currentParticle)
    {
        currentParticle.Play();
        yield return new WaitForSeconds(1f);
        if (currentParticle != null)
        {
            Destroy(currentParticle.gameObject);
        }
    }

    IEnumerator FallAnimation(Transform tpos)
    {
        for (int i = 0; i < 10f; i++) 
        {
            Vector3 scaler = transform.localScale * .75f;
            transform.localScale = scaler;
            Vector3 prevpos = transform.position;
            Vector3 newpos = new Vector3(tpos.position.x - UnityEngine.Random.Range(-.05f, .05f), tpos.position.y - UnityEngine.Random.Range(-.05f, .05f), -1);
            transform.position = newpos;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0f, 360f)));
            yield return new WaitForSeconds(Time.deltaTime * 6);
            transform.position = prevpos;
        }
        if (gameObject.tag != "cue")
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 scale = new Vector3(.8f, .8f, 1);
            transform.localScale = scale;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        rb.angularVelocity = 0f;
    }
}
