using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foyer : MonoBehaviour
{
    public Material lightBulb;
    public Light foyerLight;
    public MeshRenderer doorSpikes;
    public MeshRenderer doorLock;
    public MeshRenderer darkMirror;

    public Light[] lightningLights;
    public Material lightningWindow;

    private bool lightsOn;
    private float lightningStrength;

    void Start()
    {
        lightBulb.SetInt("_HDRSwitch", 1);
        lightsOn = true;
        doorSpikes.enabled = false;
        doorLock.enabled = false;
        darkMirror.enabled = false;

        lightningStrength = 2.4f;
        lightningWindow.SetFloat("_LightningSwitch", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (lightsOn)
            {
                lightsOn = false;
                lightBulb.SetInt("_HDRSwitch", 0);
                foyerLight.enabled = false;
                doorSpikes.enabled = true;
                doorLock.enabled = true;
                darkMirror.enabled = true;

                StartCoroutine(LightningCheck());
                return;
            }

            lightsOn = true;
            lightBulb.SetInt("_HDRSwitch", 1);
            foyerLight.enabled = true;
            doorSpikes.enabled = false;
            doorLock.enabled = false;
            darkMirror.enabled = false;

            StopAllCoroutines();

            // Need to turn these off in case I interrupted a flash
            lightningLights[0].enabled = false;
            lightningLights[1].enabled = false;
            lightningWindow.SetFloat("_LightningSwitch", 0);

        }
    }

    IEnumerator LightningCheck()
    {
        while(true)
        {
            // Every 10 seconds there will be a 100% chance of lightning
            yield return new WaitForSeconds(10f);
            float lightningTest = Random.Range(0f, 1f);
            if (lightningTest <= 1.0f)
            {
                StartCoroutine(LightningFlash());
            }
        }
    }

    IEnumerator LightningFlash()
    {
        // Lightning flash lasts for about 2.2 seconds
        Light light1 = lightningLights[0];
        Light light2 = lightningLights[1];
        light1.intensity = lightningStrength;
        light2.intensity = lightningStrength;
        float time = 0f;
        
        light1.enabled = true;
        light2.enabled = true;
        lightningWindow.SetFloat("_LightningSwitch", 1);
        yield return new WaitForSeconds(0.07f);

        light1.enabled = false;
        light2.enabled = false;
        lightningWindow.SetFloat("_LightningSwitch", 0);
        yield return new WaitForSeconds(0.07f);

        light1.enabled = true;
        light2.enabled = true;
        lightningWindow.SetFloat("_LightningSwitch", 1);
        while (time < 2f)
        {
            float dt = Time.deltaTime;
            float lerpPercent = time * 0.5f;
            float curStrength = Mathf.Lerp(lightningStrength, 0f, lerpPercent);
            light1.intensity = curStrength;
            light2.intensity = curStrength;
            lightningWindow.SetFloat("_LightningSwitch", 1 - lerpPercent);

            time += dt;
            yield return null;
        }

        light1.enabled = false;
        light2.enabled = false;
        lightningWindow.SetFloat("_LightningSwitch", 0);
    }
}
