using System.Collections;
using UnityEngine;

namespace GameLogic.Physics
{

    [System.Serializable]
    public class JumpModule
    {
        public SoldierData.Data soldierData;
        public JumpModuleData jumpModuleData;

        public float HScaleRatio { get { return jumpModuleData.heightScaleRatio; } }
        public float Power { get { return soldierData.equipment.jetPack.power; } }
        public float Weight { get { return WeightModule.GetTotalSoldierWeight(soldierData); } }
        public float MinJumpRange { get { return CalcParam(jumpModuleData.minRange, Weight, Power); } }
        public float MaxJumpRange { get { return CalcParam(jumpModuleData.maxRange, Weight, Power); } }
        public Vector2 OrignJumpHeight
        {
            get
            {
                float x = MaxJumpRange * jumpModuleData.orignHeightRatio;
                float y = CalcParam(jumpModuleData.orignHeight, Weight, Power);
                return new Vector2(x,y);
            }
        }

        public void Init(SoldierData.Data soldierData, JumpModuleData jpData)
        {
            this.jumpModuleData = jpData;
            this.soldierData = soldierData;
        }

        static public float CalcParam(JumpModuleParam jmp, float weight, float power)
        {
            return jmp.fMultipler * Mathf.Pow(power, jmp.fPowPower) / Mathf.Pow(weight, jmp.fPowWeight);
        }



    }
}