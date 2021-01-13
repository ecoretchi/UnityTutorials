using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmUtility
{
    public static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string LowercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToLower(s[0]) + s.Substring(1);
    }

    public static List<int> ShuffledSequence(int len)
    {
        List<int> v = new List<int>();
        for (int i = 0; i < len; ++i)
            v.Add(i);
        List<int> resultArray = new List<int>();
        for (int i = 0; i < len; ++i)
        {
            int randIndex = Random.Range(0, v.Count);
            resultArray.Add(v[randIndex]);
            v.RemoveAt(randIndex);
        }
        return resultArray;
    }

    public static List<T> ShuffledSequence<T>(List<T> sq)
    {
        int len = sq.Count;
        List<int> v = new List<int>();
        for (int i = 0; i < len; ++i)
            v.Add(i);
        List<T> resultArray = new List<T>();
        for (int i = 0; i < len; ++i)
        {
            int randIndex = Random.Range(0, v.Count);
            resultArray.Add(sq[v[randIndex]]);
            v.RemoveAt(randIndex);
        }
        return resultArray;
    }
    
    public static List<Vector2Int> Noise(int width, int length, float power)
    {
        List<Vector2Int> v = new List<Vector2Int>();
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)

                if (Random.value > 1 - power)
                {
                    v.Add(new Vector2Int(i, j));
                }
        }
        return v;
    }


}