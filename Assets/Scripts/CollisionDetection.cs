using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public int walls_Hit = 0;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (CueBallBehavior.scratched == false)
        {
            if (coll.gameObject.tag == "cue")
            {
                walls_Hit++;
            }
        }
    }
}
