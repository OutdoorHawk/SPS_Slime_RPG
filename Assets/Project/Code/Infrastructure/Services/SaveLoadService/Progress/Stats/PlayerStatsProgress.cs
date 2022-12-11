using System;
using Project.Code.Infrastructure.Data;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats
{
    [Serializable]
    public class PlayerStatsProgress
    {
       [SerializeField] private StatsProgression _statsProgression;

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