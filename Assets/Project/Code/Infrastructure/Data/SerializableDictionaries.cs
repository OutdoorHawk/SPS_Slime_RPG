using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;

namespace Project.Code.Infrastructure.Data
{
    [Serializable]
    public class StatsProgression : UnitySerializedDictionary<StatID, StatProgress>
    {
    }
}