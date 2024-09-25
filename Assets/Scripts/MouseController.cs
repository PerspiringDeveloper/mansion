using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform mouseHole, sewingMachine, ironingTable;
    public Transform mouse;
    
    void Start()
    {
        mouse.localScale = Vector3.zero;
        StartCoroutine(MouseRoutine());
    }

    IEnumerator MouseRoutine()
    {
        Vector3 target;
        Vector3 start;
        while (true)
        {
            bool toMouseHole = Random.value < 0.5f ? true : false;
            bool sewingOrIroning = Random.value < 0.5f ? true : false;
            if (toMouseHole) {
                target = mouseHole.position;
                start = sewingOrIroning == true ? sewingMachine.position : ironingTable.position;
            } else {
                start = mouseHole.position;
                target = sewingOrIroning == true ? sewingMachine.position : ironingTable.position;
            }

            mouse.position = start;
            mouse.localScale = Vector3.one;
            mouse.forward = target - start;

            float time = 0f;
            while (time < 3f)
            {
                mouse.position = Vector3.Lerp(start, target, time * .33f);
                time += Time.deltaTime;
                yield return null;
            }

            mouse.localScale = Vector3.zero;
            yield return new WaitForSeconds(5f);
        }
    }
}
