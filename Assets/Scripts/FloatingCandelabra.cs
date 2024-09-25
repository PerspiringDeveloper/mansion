using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCandelabra : MonoBehaviour
{
    public float amplitude, frequency;
    public float moveSpeed;

    private float startHeight;

    void Start()
    {
        startHeight = transform.position.y;
        StartCoroutine(PaceRoutine());
    }

    void Update()
    {
        float modTime = Time.fixedTime % 360f;
        transform.position = new Vector3(
            transform.position.x,
            startHeight + Mathf.Sin(modTime * frequency) * amplitude,
            transform.position.z
        );
    }

    IEnumerator PaceRoutine()
    {
        float time, temp;
        float startX = transform.position.x;
        float endX = startX - 1.3f;

        Quaternion curRotation, newRotation;

        while (true)
        {
            time = 0f;
            while (time < 1f) 
            {
                time += Time.deltaTime * moveSpeed;
                transform.position = new Vector3(
                    Interp(startX, endX, time),
                    transform.position.y,
                    transform.position.z
                );
                yield return null;
            }

            time = 0f;
            curRotation = transform.rotation;
            newRotation = curRotation * Quaternion.Euler(0f, 180f, 0f);
            while (time < 1f)
            {
                time += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(curRotation, newRotation, time);
                yield return null;
            }

            temp = startX;
            startX = endX;
            endX = temp;
        }
    }

    private float Interp(float startX, float endX, float percent)
    {
        if (percent >= 1) return endX;
        return startX + (endX - startX) * percent;
    }

}
