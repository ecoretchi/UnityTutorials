using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JumpModuleParam
{
    [Range(0.01f,1000)]
    public float fMultipler;
    [Range(0, 3)]
    public float fPowWeight;
    [Range(0, 3)]
    public float fPowPower;
}

[System.Serializable]
public class JumpModuleData
{
    public JumpModuleParam minRange;
    public JumpModuleParam maxRange;
    public JumpModuleParam orignHeight;
    [Range(0, 1)]
    public float orignHeightRatio;

    [Header("Cubic curve Horizontal smooth")]
    [Range(0,2)]
    public float softRatioH;
    [Range(0, 1)]
    public float shiftRatioH;
    [Header("Cubic curve Vertical smooth")]
    [Range(0, 2)]
    public float softRatioV;
    [Range(0, 1)]
    public float shiftRatioV;
    [Range(0, 2)]
    public float softRatioVx;
    [Range(0, 1)]
    public float shiftRatioVx;

    public BezierCurveType bezierType;
    public float heightScaleYRatio;
    public float heightScaleXRatio;
}
