using System.Collections;
using UnityEngine;

namespace GameLogic.Physics
{
    public class WeightModule
    {
        static public float GetTotalSoldierWeight(SoldierData.Data soldierData)
        {
            float weight = soldierData.property.weight;
            if (soldierData.equipment.jetPack != null)
                weight += soldierData.equipment.jetPack.weight;

            // TODO: Impl here
            return weight;
        }
    }
}