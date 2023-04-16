using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public GameObject[] MaskObj;

    private void Start()
    {
        for (int i = 0; i < MaskObj.Length; i++)
        {
            MaskObj[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }
}
