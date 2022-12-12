using System;

namespace Project.Code.StaticData.World
{
    [Serializable]
    public class LevelConfig
    {
        public float StatMultiplier = 1;
        public LevelStaticData LevelStaticData;
    }
}