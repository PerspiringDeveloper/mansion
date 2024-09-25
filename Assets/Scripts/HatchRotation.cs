using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchRotation : MonoBehaviour
{
    public float rotationAmount;
    private Quaternion startRot, endRot;

    void Start()
    {
        startRot = transform.localRotation;
        endRot = startRot * Quaternion.Euler(0f, 0f, rotationAmount);
    }

    public void Rotate(float percent)
    {
        transform.localRotation = Quaternion.Lerp(startRot, endRot, percent);
    }

}
