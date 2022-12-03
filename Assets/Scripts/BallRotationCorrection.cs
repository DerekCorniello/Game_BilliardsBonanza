using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotationCorrection : MonoBehaviour
{
    private Rigidbody2D rb;
    private Quaternion targetrotation;
    public float smoothRotateSpeed = .1f;
    public CueBallBehavior CueBallBehavior;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if (CueBallBehavior.cueHit)
        {
            if (rb.velocity != new Vector2(0f, 0f))
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, rb.velocity);
            }
        }
    }
}
