using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerCurrencyProgress PlayerCurrencyProgress;
        public PlayerStatsProgress PlayerStatsProgress;
        public PlayerLevelsProgress PlayerLevelsProgress;

        public PlayerProgress()
        {
            PlayerCurrencyProgress = new PlayerCurrencyProgress();
            PlayerStatsProgress = new PlayerStatsProgress();
            PlayerLevelsProgress = new PlayerLevelsProgress();
        }
    }
}