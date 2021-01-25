using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class JumpModuleTest : MonoBehaviour
{

    public bool showCurrentHeight;
    public bool showMinHeight;
    public bool showRefPoints;
    public bool showTrajectory;
    public bool showMinRange;
    public bool showMaxRange;



    [Range(80, 150)]
    public int Weight  = 10;
    [Range(40, 120)]
    public int Power = 10;

    [Range(1, 22)]
    public int jumpTo;
    [Range(0.1f,1)]
    public float  jumpStepDiv;
    
    public Vector2 minimaxHeightResult;
    public float maxRangeResult;
    public float minRangeResult;
    [Range(2, 20)]
    public int CurvesSteps = 10;
    /*test*/
    Vector3 mousePosition;

    public JumpModuleData defJumpModuleData;

    JumpModule jumpModule = new JumpModule();
    JumpCurvesModule curvesModule = new JumpCurvesModule();

    private float range = 0.0f;

    void Awake()
    {
        defJumpModuleData = new JumpModuleData()
        {
            bezierType = BezierCurveType.Quad,
            minRange = new JumpModuleParam() { fMultipler = 40, fPowWeight = 2, fPowPower = 1.35f },
            maxRange = new JumpModuleParam() { fMultipler = 65, fPowWeight = 1.5f, fPowPower = 1 },
            orignHeight = new JumpModuleParam() { fMultipler = 2, fPowWeight = 1.42f, fPowPower = 1.5f },
            orignHeightRatio = 0.5f,
            softRatioH = 1,
            softRatioV = 1,
            softRatioVx = 1,
            shiftRatioH = 0.5f,
            shiftRatioV = 0.5f,
            shiftRatioVx = 0.5f,
            heightScaleXRatio = 1,
            heightScaleYRatio = 2
        };
    }

    public void UpdateTrajectory()
    {
        jumpModule.Init(defJumpModuleData);
        curvesModule.Init(jumpTo * jumpStepDiv, jumpModule, CurvesSteps);
        curvesModule.Calc();
    }
    void OnDrawGizmosSelected()
    {
        EditorPainter.position = transform.position;
        EditorPainter.rotate = transform.rotation;
        UpdateGizmos();
    }


    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Destroy(this);
        }
        SceneView.duringSceneGui += OnScene;
    }

    void OnScene(SceneView scene)
    {
        range = range + Time.deltaTime;

        if (range > 0.1f)
        {
            mousePosition = Event.current.mousePosition;
            range = 0.0f;

        }
    }

    private void OnValidate()
    {
        if (jumpModule != null)
        {
            jumpModule.Weight = Weight;
            jumpModule.Power = Power;
            UpdateTrajectory();
        }
    }
    
    Vector3 ZeroPos = Vector3.zero;

    void UpdateGizmos()
    {
        EditorPainter.PaintByGizmo = true;

        DrawRefPoints();
        //test//DrawRayHitPointOnSurface();

        minimaxHeightResult = jumpModule.OrignJumpHeight;
        maxRangeResult = jumpModule.MaxJumpRange;
        minRangeResult = jumpModule.MinJumpRange;

        EditorPainter.DrawCircle(ZeroPos, 0.3f, Color.blue);

        if (showMinRange)
        {
            float minJP = jumpModule.MinJumpRange;
            EditorPainter.DrawCircle(ZeroPos, minJP, Color.red);
        }

        if (showMaxRange)

        {
            float maxJP = jumpModule.MaxJumpRange;
            EditorPainter.DrawCircle(ZeroPos, maxJP, Color.green);
        }        

        if(showCurrentHeight)
        {
            var p = ZeroPos;
            p.y += curvesModule.currentH.y;
            EditorPainter.DrawCircle(p, curvesModule.currentH.x, Color.white);
        }

        if (showMinHeight)
            DrawHeights();
        if(showTrajectory)
            DrawJumpCurve();
    }
    void DrawRefPoints()
    {
        if(showRefPoints)
        {
            EditorPainter.DrawLines(curvesModule.RefPoints, ZeroPos, Color.red);
            foreach (var p in curvesModule.RefPoints)
            {
                EditorPainter.DrawSphere(p, 0.05f, Color.yellow);
            }
        }
    }

    void DrawRayHitPointOnSurface()
    {
        // TODO: Test to make working
        Vector3 mousePosition;
        mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);

        RaycastHit hitInfo;
        int layerMask = -1;// 1 << LayerMask.NameToLayer("Gizmo");
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(hitInfo.point, Vector3.up * 5);
        }
    }
    void DrawHeights()
    {
        float hxLen = jumpModule.OrignJumpHeight.x;
        var posDirH = new Vector3()
        {
            y = jumpModule.OrignJumpHeight.y
        };
        var posDirHL = new Vector3(hxLen, 0);
        var posDirHR = new Vector3(0, 0, hxLen);
        EditorPainter.DrawRay(ZeroPos, posDirH + posDirHL, Color.blue);
        EditorPainter.DrawRay(ZeroPos, posDirH - posDirHL, Color.blue);
        EditorPainter.DrawRay(ZeroPos, posDirH + posDirHR, Color.blue);
        EditorPainter.DrawRay(ZeroPos, posDirH - posDirHR, Color.blue);

        EditorPainter.DrawRay(ZeroPos - posDirHL, posDirH, Color.blue);
        EditorPainter.DrawRay(ZeroPos + posDirHL, posDirH, Color.blue);
        EditorPainter.DrawRay(ZeroPos - posDirHR, posDirH, Color.blue);
        EditorPainter.DrawRay(ZeroPos + posDirHR, posDirH, Color.blue);

        EditorPainter.DrawCircle(ZeroPos + posDirH, hxLen, Color.blue);

    }

    void DrawJumpCurve()
    {
        EditorPainter.DrawLines(curvesModule.Result, ZeroPos, Color.green);
        
    }
}
