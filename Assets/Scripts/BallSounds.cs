using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    public AudioManagerScript aud;
    public CueBallBehavior CueBallBehavior;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!CueBallBehavior.scratched)
        {
            if (col.gameObject.tag == "redball" || col.gameObject.tag == "blueball" || col.gameObject.tag == "eight" || col.gameObject.tag == "cue")
            {
                int randInt = Random.Range(1, 5);
                string randString = "ballclick" + randInt.ToString();
                aud.Play(randString);
            }

            else if (col.gameObject.tag == "wall")
            {
                int randInt = Random.Range(1, 4);
                string randString = "sidekiss" + randInt.ToString();
                aud.Play(randString);
            }
        }
    }
}
