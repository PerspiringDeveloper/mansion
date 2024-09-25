using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    public Light spotlight;
    public MeshRenderer cone;

    private bool on;

    void Start()
    {
        //StartCoroutine(Flicker());
        on = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("5"))
        {
            on = !on;
            spotlight.enabled = on;
            cone.enabled = on;
        }
    }

    //This will cause seizures so I'm not using it
    IEnumerator Flicker()
    {
        bool enabled = false;
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;

            if (time > 0.1f)
            {
                enabled = !enabled;
                spotlight.enabled = enabled;
                cone.enabled = enabled;
                time = 0f;
            }

            yield return null;
        }
    }
}
