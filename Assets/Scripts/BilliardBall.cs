using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardBall : MonoBehaviour
{
    public float[] xBounds, yBounds, zBounds;
    public Vector3 dir;
    public float speed;

    private Vector3 start;
    private bool moving;

    void Start()
    {
        start = transform.localPosition;
        moving = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("6"))
        {
            if (moving) { transform.localPosition = start; }
            moving = !moving;
        }

        if (moving) { transform.localPosition += dir * speed * Time.deltaTime; }

        // x-Bounds
        if (transform.localPosition.x > xBounds[1] && dir.x > 0)
        {
            dir.x = -dir.x;
        }
        if (transform.localPosition.x < xBounds[0] && dir.x < 0)
        {
            dir.x = -dir.x;
        }

        // y-Bounds
        if (transform.localPosition.y > yBounds[1] && dir.y > 0)
        {
            dir.y = -dir.y;
        }
        if (transform.localPosition.y < yBounds[0] && dir.y < 0)
        {
            dir.y = -dir.y;
        }

        // z-Bounds
        if (transform.localPosition.z > zBounds[1] && dir.z > 0)
        {
            dir.z = -dir.z;
        }
        if (transform.localPosition.z < zBounds[0] && dir.z < 0)
        {
            dir.z = -dir.z;
        }
    }
}
