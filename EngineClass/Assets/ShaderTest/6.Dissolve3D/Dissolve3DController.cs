using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve3DController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Single value;
    private bool dissolve;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetFloat("_Dissolve", 0);
    }
    private void Update()
    {
        if (dissolve)
            value += Time.deltaTime;
        else
            value -= Time.deltaTime;

        if (value <= 0)
            dissolve = true;
        else if (value >= 1)
            dissolve = false;

        meshRenderer.material.SetFloat("_Dissolve", value);
    }

}
