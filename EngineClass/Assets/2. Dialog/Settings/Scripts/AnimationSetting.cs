using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="AnimationSetting",menuName ="Custom/AnimationSetting",order=Int32.MaxValue)]
public class AnimationSetting : ScriptableObject
{
    [SerializeField] private AnimationCurve m_curve;

    public Single Evaluate(Single pTime)
    {
        return m_curve.Evaluate(pTime);
    }
}