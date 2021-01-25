using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JumpCurvesModule
{
    public List<Vector3> Result = new List<Vector3>();
    public List<Vector3> RefPoints = new List<Vector3>();
    public Vector2 currentH;

    JumpModule jumpModule;

    int steps = 10;

    float jumpToR;
    float maxRange;
    float minRange;

    float softCurveH = 0.62f;
    float shiftCurveH = 0.62f;
    float softCurveV = 0.62f;
    float shiftCurveV = 0.62f;
    float softCurveVx = 0.62f;
    float shiftCurveVx = 0.62f;

    void Validate()
    {
        maxRange = jumpModule.MaxJumpRange;
        minRange = jumpModule.MinJumpRange;

        var ratioX = jumpModule.jumpModuleData.heightScaleXRatio;
        var ratioY = jumpModule.jumpModuleData.heightScaleYRatio;
        var scale = jumpToR / maxRange;

        currentH = jumpModule.OrignJumpHeight;

        currentH.x = Mathf.Lerp(0, currentH.x * ratioX, scale);
        currentH.y = Mathf.Lerp(currentH.y, currentH.y * ratioY, 1 - scale);

        softCurveH = jumpModule.jumpModuleData.softRatioH;
        shiftCurveH = jumpModule.jumpModuleData.shiftRatioH;

        softCurveV = jumpModule.jumpModuleData.softRatioV;
        shiftCurveV = jumpModule.jumpModuleData.shiftRatioV;

        softCurveVx = jumpModule.jumpModuleData.softRatioVx;
        shiftCurveVx = jumpModule.jumpModuleData.shiftRatioVx;
    }

    public void SetJumpTo(float r)
    {
        this.jumpToR = r;
        Validate();
    }

    public void SetJumpTo(float r, int steps)
    {
        this.steps = steps;
        this.jumpToR = r;
        Validate();
    }

    public void Init(float jumpToR, JumpModule jumpModule, int steps)
    {
        this.jumpModule = jumpModule;
        this.steps = steps;
        this.jumpToR = jumpToR;
        Validate();
    }

    public void Init(JumpModule jumpModule, int steps)
    {
        this.jumpToR = jumpModule.MaxJumpRange;
        this.jumpModule = jumpModule;
        this.steps = steps;
        Validate();
    }

    public void Calc()
    {
        Result.Clear();
        RefPoints.Clear();
        CalcFirstCurve();
        CalcSecondCurve();
    }

    void CalcFirstCurve()
    {
        var h = currentH;

        var refP = new List<Vector3>();

        var curvesType = jumpModule.jumpModuleData.bezierType;

        switch (curvesType)
        {
            case BezierCurveType.Quad:
                {
                    var p0 = Vector3.zero;
                    var p1 = new Vector3(0, h.y);
                    var p2 = new Vector3(h.x, h.y);

                    refP.Add(p0);
                    refP.Add(p1);
                    refP.Add(p2);
                }
                break;
            case BezierCurveType.Cubic:
                {
                    float ratioX = Mathf.Lerp(0, softCurveH, shiftCurveH);
                    float ratioY = Mathf.Lerp(0, softCurveV, shiftCurveV);
                    float ratioYx = Mathf.Lerp(0, softCurveVx, shiftCurveVx);
                    var p0 = Vector3.zero;
                    var p1 = new Vector3((ratioYx - 0.5f) * h.x, h.y * ratioY);
                    var p2 = new Vector3(h.x - (h.x) * ratioX, h.y);
                    var p3 = new Vector3(h.x, h.y);

                    refP.Add(p0);
                    refP.Add(p1);
                    refP.Add(p2);
                    refP.Add(p3);
                }
                break;
        }
        RefPoints.AddRange(refP);
        Result.AddRange(BezierCurves.CalcPoints(refP, curvesType, steps));

    }

    void CalcSecondCurve()
    {
        var h = currentH;
        float r = jumpToR;

        var refP = new List<Vector3>();
        switch (jumpModule.jumpModuleData.bezierType)
        {
            case BezierCurveType.Quad:
                {
                    var p0 = new Vector3(h.x, h.y);
                    var p1 = new Vector3(r, h.y);
                    var p2 = new Vector3(r, 0);
                    refP.Add(p0);
                    refP.Add(p1);
                    refP.Add(p2);
                }
                break;
            case BezierCurveType.Cubic:
                {
                    float ratioX = Mathf.Lerp(softCurveH, 0, shiftCurveH);
                    float ratioY = Mathf.Lerp(softCurveV, 0, shiftCurveV);
                    float ratioYx = Mathf.Lerp(softCurveVx, 0, shiftCurveVx);

                    var p0 = new Vector3(h.x, h.y);
                    var p1 = new Vector3(h.x + (r - h.x) * ratioX, h.y);
                    var p2 = new Vector3(r + (0.5f - ratioYx) * h.x, h.y * ratioY);
                    var p3 = new Vector3(r, 0);
                    refP.Add(p0);
                    refP.Add(p1);
                    refP.Add(p2);
                    refP.Add(p3);
                }
                break;
        }
        RefPoints.AddRange(refP);
        Result.AddRange(BezierCurves.CalcPoints(refP, jumpModule.jumpModuleData.bezierType, steps));
    }


}
