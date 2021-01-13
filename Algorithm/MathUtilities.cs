using System.Collections;
using UnityEngine;

public class MathUtilities 
{

    public static Vector2Int CeilXZToV2I(Vector3 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.z));
    }

    public static Vector2Int CeilXYToV2I(Vector3 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
    }

    public static Vector2Int CeilYZToV2I(Vector3 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
    }

    public static Vector2Int CeilYXToV2I(Vector3 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.x));
    }

    public static Vector2Int CeilZXToV2I(Vector3 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.z), Mathf.CeilToInt(v.x));
    }

    public static Vector2Int CeilZYToV2I(Vector3 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.z), Mathf.CeilToInt(v.y));
    }

    public static Vector2Int CeilToV2I(Vector2 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
    }
}