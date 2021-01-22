using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPainter
{
    public static void DrawCircle(Vector3 center, float radius, Color color)
    {
        Vector3 prevPos = center + new Vector3(radius, 0, 0);
        for (int i = 0; i < 30; i++)
        {
            float angle = (float)(i + 1) / 30.0f * Mathf.PI * 2.0f;
            Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Debug.DrawLine(prevPos, newPos, color);
            prevPos = newPos;
        }
    }
    public static void DrawLines( List<Vector3> points, Vector3 offset, Color color)
    {
        if (points.Count < 2)
            return;
        for(int i = 1; i < points.Count;++i)
        {
            var p1 = points[i-1];
            var p2 = points[i];
            Debug.DrawLine(offset+p1, offset+p2, color);
        }
    }
}