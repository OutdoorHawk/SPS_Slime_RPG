using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerCurrencyProgress PlayerCurrencyProgress;
        public PlayerStatsProgress PlayerStatsProgress;

        public PlayerProgress()
        {
            PlayerCurrencyProgress = new PlayerCurrencyProgress();
            PlayerStatsProgress = new PlayerStatsProgress();
        }
    }
}