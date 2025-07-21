using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Single value;
    private bool dissolve;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_Dissolve", 0);
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

        spriteRenderer.material.SetFloat("_Dissolve", value);
    }

}
