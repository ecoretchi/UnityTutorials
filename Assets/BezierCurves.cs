using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum BezierCurveType
{
    Quad,
    Cubic
}

public class BezierCurves
{
    public static Vector3 GetQuadCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        t = Mathf.Clamp(t, 0, 1);

        var k0 = (1 - t) * (1 - t);
        var k1 = 2.0f * t * (1 - t);
        var k2 = t * t;
        var p = (k0 * p0) + (k1 * p1) + (k2 * p2);
        return p;
    }

    public static Vector3 GetCubicCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        t = Mathf.Clamp(t, 0, 1);

        var k0 = (1 - t) * (1 - t) * (1 - t);
        var k1 = 3.0f * t * (1 - t) * (1 - t);
        var k2 = 3.0f * t * t * (1 - t);
        var k3 = t * t * t;
        var p = (k0 * p0) + (k1 * p1) + (k2 * p2) + (k3 * p3);
        return p;
    }

    public static List<Vector3> CalcPoints(List<Vector3> refP, BezierCurveType curveType, int steps = 10)
    {
        var resultPoints = new List<Vector3>();
        Vector3 nextPoint = Vector3.zero;

        for (int i = 0; i <= steps; ++i)
        {
            float t = i / (float)steps;
            switch (curveType)
            {
                case BezierCurveType.Quad:
                    nextPoint = GetQuadCurvePoint(t, refP[0], refP[1], refP[2]);
                    break;
                case BezierCurveType.Cubic:
                    nextPoint = GetCubicCurvePoint(t, refP[0], refP[1], refP[2], refP[3]);
                    break;
                default:
                    throw new System.Exception("Impl missing, or logic broken");
            }
            resultPoints.Add(nextPoint);
        }
        return resultPoints;
    }
}
