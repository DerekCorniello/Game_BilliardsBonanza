using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionBalls : MonoBehaviour
{
    public CueBallBehavior CueBallBehavior;

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (CueBallBehavior.scratched == false)
        {
            if (coll.gameObject.tag == "cue")
            {
                CueBallBehavior.ballHit = true;
            }
        }
    }
}
