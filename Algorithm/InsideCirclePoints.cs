using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algorithm
{
    public class InsideCirclePoints
    {
        public Vector2Int centerPos = Vector2Int.zero;
        public List<Vector2Int> result = new List<Vector2Int>();
        public static InsideCirclePoints Generate(float radius)
        {
            InsideCirclePoints thisObj = new InsideCirclePoints();

            float r = Mathf.CeilToInt(radius);
            float rf = radius + 0.3f;
            int iR2 = Mathf.CeilToInt(2 * r);
            float fR1 = iR2 * 0.5f;
            for (int i = 0; i <= iR2; ++i)
                for (int j = 0; j <= iR2; ++j)
                {
                    Vector2 currentPos = new Vector2( i - fR1, j - fR1);
                    var r1 = currentPos.sqrMagnitude;
                    
                    if(r1 <= rf * rf)
                    {
                        thisObj.result.Add(MathUtilities.CeilToV2I(currentPos));
                    }
                }

            return thisObj;
        }
    }
}