using System;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats
{
    [Serializable]
    public class StatProgress
    {
        public float StatValue;
        public int StatLvl = 1;
        public int StatUpgradeCost;
    }
}