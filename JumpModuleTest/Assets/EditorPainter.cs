using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPainter
{
    public static Quaternion rotate;
    public static Vector3 position;

    public static bool PaintByGizmo = false;

    static Vector3 Transform( Vector3 p1)
    {
        var p0 = position;
        p1 = rotate * p1; 
        
        return p1 + p0;
    }
    public static void DrawSphere(Vector3 start, float radius, Color color)
    {
        start = Transform(start);


        if (PaintByGizmo)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(start, radius);
        }
        else
        {
            // No implemetation Yet
        }

    }
    public static void DrawRay(Vector3 start, Vector3 dir, Color color)
    {
        start = Transform(start);
        dir = rotate * dir;

        if (PaintByGizmo)
        {

            Gizmos.color = color;
            Gizmos.DrawRay(start, dir);
        }
        else
        {
            Debug.DrawRay(start, dir, color);
        }
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        start = Transform(start);
        end = Transform(end);

        if (PaintByGizmo)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(start, end);
        }
        else
        {
            Debug.DrawLine(start, end, color);
        }
    }

    public static void DrawCircle(Vector3 center, float radius, Color color)
    {
        Vector3 prevPos = center + new Vector3(radius, 0, 0);
        for (int i = 0; i < 30; i++)
        {
            float angle = (float)(i + 1) / 30.0f * Mathf.PI * 2.0f;
            Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            DrawLine(prevPos, newPos, color);
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
            DrawLine(offset+p1, offset+p2, color);
        }
    }
}