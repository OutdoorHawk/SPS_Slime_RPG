using System;

namespace Project.Code.StaticData.Units.Player
{
    [Serializable]
    public class StatData
    {
        public float StatBaseValue;
        public int StatBaseUpgradeCost;
        
        public float StatValueIncrease;
        public int StatUpgradeCostIncrease;
    }
}