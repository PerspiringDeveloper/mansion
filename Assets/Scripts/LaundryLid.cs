using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryLid : MonoBehaviour
{
    public Vector3 endLocation, endRotation;

    private Vector3 startLocation;
    private Quaternion startRotation;
    private bool closed, moving;

    void Start()
    {
        startLocation = transform.localPosition;
        startRotation = transform.rotation;
        closed = true;
        moving = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            if (closed && !moving)
            {
                moving = true;
                closed = !closed;
                StartCoroutine(MoveLid(
                    startLocation, startRotation, endLocation, Quaternion.Euler(endRotation)
                ));
                return;
            }
            if (!closed && !moving)
            {
                moving = true;
                closed = !closed;
                StartCoroutine(MoveLid(
                    endLocation, Quaternion.Euler(endRotation), startLocation, startRotation
                ));
                return;
            }
        }
    }

    IEnumerator MoveLid(Vector3 startL, Quaternion startR, Vector3 endL, Quaternion endR)
    {
        float time = 0f;
        while (time < 1f)
        {
            float lerpPercent = Mathf.Pow(time, 2f);
            transform.localPosition = Vector3.Lerp(startL, endL, lerpPercent);
            transform.localRotation = Quaternion.Lerp(startR, endR, lerpPercent);

            time += Time.deltaTime;
            yield return null;
        }

        moving = false;
    }
}
