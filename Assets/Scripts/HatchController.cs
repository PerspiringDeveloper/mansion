using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : MonoBehaviour
{
    public HatchRotation[] hatches;
    public ParticleSystem particles;
    public float lerpSpeed;

    private bool moving, open;

    void Start()
    {
        moving = false;
        open = false;
        particles.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown("4") && !moving)
        {
            moving = true;
            if (open) {
                StartCoroutine(CloseRoutine(lerpSpeed));
                particles.Stop();
            } else {
                StartCoroutine(OpenRoutine(lerpSpeed));
                particles.Play();
            }
        }
    }

    IEnumerator OpenRoutine(float speed)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            foreach (HatchRotation hatch in hatches)
            {
                hatch.Rotate(time);
            }

            yield return null;
        }

        moving = false;
        open = !open;
    }

    IEnumerator CloseRoutine(float speed)
    {
        float time = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime * speed;
            foreach (HatchRotation hatch in hatches)
            {
                hatch.Rotate(time);
            }
            
            yield return null;
        }

        moving = false;
        open = !open;
    }
}
