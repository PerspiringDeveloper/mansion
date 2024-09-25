using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speen : MonoBehaviour
{
    public Vector3 speen;

    void Update()
    {
        transform.Rotate(speen * Time.deltaTime);
    }
}
