using System;

namespace Project.Code.StaticData.Units.Player
{
    [Serializable]
    public class StatData
    {
        public float BaseValue;
        public int BaseUpgradeCost;
        
        public float ValueIncrease;
        public int UpgradeCostIncrease;
        
        public int MaxLvl = 100;
    }
}