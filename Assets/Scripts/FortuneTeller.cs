using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneTeller : MonoBehaviour
{
    public Light fortuneLight;
    public float iMin, iMax;
    public float interval;
    private float oneOverInterval;

    void Start()
    {
        oneOverInterval = 1f / interval;
        StartCoroutine(OscillateLight());
    }

    IEnumerator OscillateLight()
    {
        float time;
        float dt;
        while (true)
        {
            time = interval;
            fortuneLight.intensity = iMax;
            while (time > 0f)
            {
                fortuneLight.intensity = Mathf.Lerp(iMin, iMax, time * oneOverInterval);
                dt = Time.deltaTime;
                time -= dt;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);

            time = 0f;
            fortuneLight.intensity = iMin;
            while (time < interval)
            {
                fortuneLight.intensity = Mathf.Lerp(iMin, iMax, time * oneOverInterval);
                dt = Time.deltaTime;
                time += dt;
                yield return null;
            }
            yield return new WaitForSeconds(0.4f); // pause for effect
        }
    }
}
