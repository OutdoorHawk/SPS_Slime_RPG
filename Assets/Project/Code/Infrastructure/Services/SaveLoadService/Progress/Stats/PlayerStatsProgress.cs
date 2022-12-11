using Project.Code.Infrastructure.Data;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats
{
    public class PlayerStatsProgress
    {
        private readonly StatsProgression _statsProgression;

        public PlayerStatsProgress()
        {
            _statsProgression = new StatsProgression
            {
                { StatID.ATK, new StatsProgress() },
                { StatID.ASPD, new StatsProgress() },
                { StatID.HP, new StatsProgress() },
                { StatID.HPREC, new StatsProgress() }
            };
        }

        public StatsProgress GetStatProgress(StatID id)
        {
            return _statsProgression[id];
        }

        public void UpgradeStat(StatID id)
        {
            _statsProgression[id].StatLvl++;
            _statsProgression[id].StatValue++;
        }
    }
}