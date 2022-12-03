using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 0f;
    public Animator Anim;
    public CueBallBehavior CueBallBehavior;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        speed = rb.velocity.magnitude;
        Anim.SetFloat("rb", speed);

        Anim.speed = rb.velocity.magnitude * .4f;
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if (CueBallBehavior.cueHit == true)
        {
            StartCoroutine(initialContactEnumerator());
        }
    }

    IEnumerator initialContactEnumerator()
    {
        Anim.SetBool("initialContact", true);
        yield return new WaitForSeconds(.1f);
        Anim.SetBool("initialContact", false);
    }
}