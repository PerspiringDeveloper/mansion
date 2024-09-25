using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCircling : MonoBehaviour
{
    public Vector3 pivot;
    public float orbitSpeed;

    void Update()
    {
        float dt = Time.deltaTime;
        Orbit(dt);
        Rotate();
    }

    private void Rotate()
    {
        Vector3 view = pivot - transform.localPosition;
        transform.rotation = Quaternion.LookRotation(view, Vector3.up);
    }

    private void Orbit(float dt)
    {
        Quaternion rotation = Quaternion.Euler(0f, orbitSpeed * dt, 0f);
        Vector3 pivotToOrigin = transform.localPosition - pivot;
        Vector3 newOrigin = (rotation * pivotToOrigin) + pivot;
        transform.localPosition = newOrigin;
    }
}
