using System.Collections;
using UnityEngine;


[System.Serializable]
public class JumpModule
{

    //public SoldierData.Data soldierData;
    public JumpModuleData jumpModuleData;

    public float Power { get; set; } = 1;
    public float Weight { get; set; } = 1;
    public float MinJumpRange { get { return CalcParam(jumpModuleData.minRange, Weight, Power); } }
    public float MaxJumpRange { get { return CalcParam(jumpModuleData.maxRange, Weight, Power); } }
    public Vector2 OrignJumpHeight
    {
        get
        {
            float x = MaxJumpRange * jumpModuleData.orignHeightRatio;
            float y = CalcParam(jumpModuleData.orignHeight, Weight, Power);
            return new Vector2(x, y);
        }
    }

    public void Init(JumpModuleData jpData)
    {
        this.jumpModuleData = jpData;
    }

    static public float CalcParam(JumpModuleParam jmp, float weight, float power)
    {
        return jmp.fMultipler * Mathf.Pow(power, jmp.fPowPower) / Mathf.Pow(weight, jmp.fPowWeight);
    }

}