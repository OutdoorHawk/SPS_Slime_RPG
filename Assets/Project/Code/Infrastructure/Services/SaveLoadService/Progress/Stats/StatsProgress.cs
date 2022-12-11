using System;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats
{
    [Serializable]
    public class StatsProgress
    {
        public float StatValue;
        public int StatLvl;
        public int StatUpgradeCost;
    }
}