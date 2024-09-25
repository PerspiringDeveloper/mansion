using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform head;
    private float time;
    private float startHeight;

    void Start()
    {
        time = 0f;
        startHeight = transform.localPosition.y;
    }

    void Update()
    {
        time += Time.deltaTime;
        time = time % 63f;
        float sinValue = Mathf.Sin(time * 2);

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            startHeight + (0.05f * Mathf.Cos(time * 2)),
            transform.localPosition.z
        );
        head.localRotation = Quaternion.Euler(
            25f * sinValue, 0f, 0f
        );
    }
}
