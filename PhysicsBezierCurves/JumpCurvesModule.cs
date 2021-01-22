using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Physics
{
    [System.Serializable]
    public class JumpCurvesModule
    {
        public List<Vector3> Result;
        public List<Vector3> RefPoints;
        public Vector2 currentH;

        JumpModule jumpModule;

        int steps = 10;

        float jumpToR;
        float maxRange;
        float minRange;

        float hRatio = 0.62f;
        float lRatio = 0.62f;

        void Validate()
        {
            maxRange = jumpModule.MaxJumpRange;
            minRange = jumpModule.MinJumpRange;

            var ratio = jumpModule.HScaleRatio;
            var scale = jumpToR / maxRange;
            currentH = jumpModule.OrignJumpHeight;

            currentH.x = Mathf.Lerp(0, currentH.x, scale);
            currentH.y = Mathf.Lerp(currentH.y, currentH.y * ratio, 1 - scale);
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

        public void Init( JumpModule jumpModule, int steps)
        {
            this.jumpToR = jumpModule.MaxJumpRange;
            this.jumpModule = jumpModule;
            this.steps = steps;
            Validate();
        }

        public void Calc()
        {
            Result = new List<Vector3>();
            RefPoints = new List<Vector3>();
            CalcFirstCurve();
            CalcSecondCurve();
        }

        void CalcFirstCurve()
        {
            var h = currentH;

            var  refP = new List<Vector3>();
            
            var curvesType = jumpModule.jumpModuleData.bezierType;

            switch (curvesType)
            {
                case Math.BezierCurveType.Quad:
                    {
                        var p0 = Vector3.zero;
                        var p1 = new Vector3(0, h.y);
                        var p2 = new Vector3(h.x, h.y);

                        refP.Add(p0);
                        refP.Add(p1);
                        refP.Add(p2);
                    }
                    break;
                case Math.BezierCurveType.Cubic:
                    {
                        var p0 = Vector3.zero;
                        var p1 = new Vector3(0, h.y * hRatio);
                        var p2 = new Vector3(h.x -(h.x)* lRatio, h.y);
                        var p3 = new Vector3(h.x, h.y);

                        refP.Add(p0);
                        refP.Add(p1);
                        refP.Add(p2);
                        refP.Add(p3);
                    }
                    break;
            }
            RefPoints.AddRange(refP);
            Result.AddRange(Math.BezierCurves.CalcPoints(refP, curvesType, steps));

        }

        void CalcSecondCurve()
        {
            var h = currentH;
            float r = jumpToR;

            var refP = new List<Vector3>();
            switch (jumpModule.jumpModuleData.bezierType)
            {
                case Math.BezierCurveType.Quad:
                    {
                        var p0 = new Vector3(h.x, h.y);
                        var p1 = new Vector3(r, h.y);
                        var p2 = new Vector3(r, 0);
                        refP.Add(p0);
                        refP.Add(p1);
                        refP.Add(p2);
                    }
                    break;
                case Math.BezierCurveType.Cubic:
                    {
                        var p0 = new Vector3(h.x, h.y);
                        var p1 = new Vector3(h.x + (r - h.x) * lRatio, h.y);
                        var p2 = new Vector3(r, h.y * hRatio);
                        var p3 = new Vector3(r, 0);
                        refP.Add(p0);
                        refP.Add(p1);
                        refP.Add(p2);
                        refP.Add(p3);
                    }
                    break;
            }
            RefPoints.AddRange(refP);
            Result.AddRange( Math.BezierCurves.CalcPoints(refP, jumpModule.jumpModuleData.bezierType, steps));
        }


    }
}