using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class JumpModuleTest : MonoBehaviour
{
    public GameLogic.Physics.JumpModule jumpModule;
    public GameLogic.Physics.JumpCurvesModule curvesModule;

    public int CurvesSteps = 10;
    [Range(1, 12)]
    public float jumpTo;

    public bool showCurrentHeight;
    public bool showMinHeight;
    public bool showTrajectory;
    public bool showMinRange;
    public bool showMaxRange;


    public Vector2 minimaxHeightResult;
    public float maxRangeResult;
    public float minRangeResult;

    void Awake()
    {
        var soldData = SoldierGenerationFactory.CreateRooky();
        var loader = new GameDataLoader<PhysicsDataPrefab>();
        loader.Load();
        jumpModule.Init(soldData, loader.DataPrefab.jumpModuleData);
    }

    public void UpdateTrajectory()
    {
        curvesModule.Init(jumpTo, jumpModule, CurvesSteps);
        curvesModule.Calc();
        
        //SendMessage("OnValidate", null, SendMessageOptions.DontRequireReceiver);
    }

    public void Save()
    {
        var loader = new GameDataLoader<PhysicsDataPrefab>();
        loader.Load();
        loader.DataPrefab.jumpModuleData = jumpModule.jumpModuleData;
        loader.Save();
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;

        foreach(var p in curvesModule.RefPoints)
        {
            Gizmos.DrawSphere(p + transform.position, 0.05f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        minimaxHeightResult = jumpModule.OrignJumpHeight;
        maxRangeResult = jumpModule.MaxJumpRange;
        minRangeResult = jumpModule.MinJumpRange;

        EditorPainter.DrawCircle(transform.position, 0.3f, Color.blue);

        if (showMinRange)
        {
            float minJP = jumpModule.MinJumpRange;
            EditorPainter.DrawCircle(transform.position, minJP, Color.red);
        }

        if (showMaxRange)

        {
            float maxJP = jumpModule.MaxJumpRange;
            EditorPainter.DrawCircle(transform.position, maxJP, Color.green);
        }        

        if(showCurrentHeight)
        {
            var p = transform.position;
            p.y = curvesModule.currentH.y;
            EditorPainter.DrawCircle(p, curvesModule.currentH.x, Color.white);
        }

        if (showMinHeight)
            DrawHeights();
        if(showTrajectory)
            DrawJumpCurve();
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
        Debug.DrawRay(transform.position, posDirH + posDirHL, Color.blue);
        Debug.DrawRay(transform.position, posDirH - posDirHL, Color.blue);
        Debug.DrawRay(transform.position, posDirH + posDirHR, Color.blue);
        Debug.DrawRay(transform.position, posDirH - posDirHR, Color.blue);

        Debug.DrawRay(transform.position - posDirHL, posDirH, Color.blue);
        Debug.DrawRay(transform.position + posDirHL, posDirH, Color.blue);
        Debug.DrawRay(transform.position - posDirHR, posDirH, Color.blue);
        Debug.DrawRay(transform.position + posDirHR, posDirH, Color.blue);

        EditorPainter.DrawCircle(transform.position + posDirH, hxLen, Color.blue);
    }

    void DrawJumpCurve()
    {
        EditorPainter.DrawLines(curvesModule.Result, transform.position, Color.green);
        EditorPainter.DrawLines(curvesModule.RefPoints, transform.position, Color.red);
    }
}
