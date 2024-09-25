using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showoff : MonoBehaviour
{
    public int[] Sizes;
    public GameObject[] Objects;

    private int sizeIndex;
    private int groupIndex;

    void Start()
    {
        sizeIndex = 0;
        groupIndex = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (sizeIndex >= Sizes.Length) {
                return;
            }

            int size = Sizes[sizeIndex];
            for (int i = groupIndex; i < groupIndex + size; i++) {
                Objects[i].SetActive(true);
            }

            groupIndex += size;
            sizeIndex += 1;
        }
    }
}
