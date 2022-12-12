using System;

namespace Project.Code.StaticData.World
{
    [Serializable]
    public class LevelConfig
    {
        public float DamageMultiplier = 1;
        public float HealthMultiplier = 1;
        public float MoneyMultiplier = 1;
        public LevelStaticData LevelStaticData;
    }
}