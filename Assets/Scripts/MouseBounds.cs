using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBounds : MonoBehaviour
{
    public static bool mouseInBounds = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "OutOfBounds")
        {
            mouseInBounds = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "OutOfBounds")
        {
            mouseInBounds = true;
        }
    }
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
