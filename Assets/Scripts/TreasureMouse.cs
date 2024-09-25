using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMouse : MonoBehaviour
{
    public float forwardSpeed;
    public float amplitude, frequency;

    private Vector3 startPos, startScale;
    private Quaternion startRot;
    private float endZ;
    private bool moving;

    void Start()
    {
        startScale = transform.localScale;
        transform.localScale = Vector3.zero;

        startPos = transform.position;
        startRot = transform.rotation;

        endZ = -4.23f;
        moving = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("3") && !moving) {
            moving = true;
            StartCoroutine(RunRoutine());
        }
    }

    IEnumerator RunRoutine()
    {
        float time = 0f;
        transform.position = startPos;
        transform.rotation = startRot;
        transform.localScale = startScale;

        while (transform.position.z > endZ)
        {
            float dt = Time.deltaTime;
            time += dt;
            float sinValue = 180f + Mathf.Sin(time * frequency) * amplitude;

            transform.rotation = Quaternion.Euler(0f, sinValue, 0f);
            transform.position = transform.position + (transform.forward * forwardSpeed * dt);

            yield return null;
        }

        transform.localScale = Vector3.zero;
        moving = false;
    }
}
