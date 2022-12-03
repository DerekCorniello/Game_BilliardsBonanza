using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueStickManager : MonoBehaviour
{
    public RackEm RackEm;
    public CueBallBehavior CueBallBehavior;
    public GameObject RedStick;
    public GameObject BlueStick;

    void Update()
    {
        if (!CueBallBehavior.gameOver)
        {
            if (!CueBallBehavior.scratched)
            {
                if (EightBallManager.selectingAPocket == false)
                {
                    if (CueBallBehavior.cueHit == false)
                    {
                        if (RackEm.turn == 1)
                        {
                            BlueStick.SetActive(true);
                            RedStick.SetActive(false);
                        }

                        if (RackEm.turn == 2)
                        {
                            BlueStick.SetActive(false);
                            RedStick.SetActive(true);
                        }
                    }
                    else
                    {
                        BlueStick.SetActive(false);
                        RedStick.SetActive(false);
                    }
                }
                else
                {
                    BlueStick.SetActive(false);
                    RedStick.SetActive(false);
                }
            }
            else
            {
                BlueStick.SetActive(false);
                RedStick.SetActive(false);
            }
        }
        else
        {
            BlueStick.SetActive(false);
            RedStick.SetActive(false);
        }
    }
}
