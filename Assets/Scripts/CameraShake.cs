using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator shake(float duration, float magnitude, float magn)
    {
        Vector3 originalPosition = new Vector3(0, .53f, -10);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude * (magn/12);
            float y = Random.Range(-1f, 1f) * magnitude * (magn/12);

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
