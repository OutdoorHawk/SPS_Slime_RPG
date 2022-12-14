using System.Collections.Generic;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.AssetProvider;
using Project.Code.Infrastructure.StaticData;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.Units.Player;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<WindowID, WindowConfig> _windows = new();
        private readonly Dictionary<UnitID, UnitStaticData> _units = new();

        private readonly IAssetProvider _assetProvider;
        private GameStaticData _data;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Load()
        {
            _data = _assetProvider.GetGameStaticData();
            LoadWindows();
            LoadUnits();
        }

        private void LoadWindows()
        {
            foreach (var window in _data.Windows)
                _windows.Add(window.ID, window);
        }

        private void LoadUnits()
        {
            foreach (var unit in _data.Units)
                _units.Add(unit.UnitID, unit.UnitStaticData);
        }

        public WindowConfig GetWindow(WindowID id) =>
            _windows.TryGetValue(id, out var windowConfig) ? windowConfig : null;

        public PlayerStaticData GetPlayerStaticData()
        {
            UnitStaticData staticData = _units.TryGetValue(UnitID.Player, out UnitStaticData unitStaticData)
                ? unitStaticData
                : null;
            return staticData as PlayerStaticData;
        }

        public WorldStaticData GetWorldStaticData()
            => _data.WorldStaticData;

        public LevelStaticData GetLevelStaticData(int level)
            => _data.LevelsStaticData[level];

        public int GetLevelsAmount() 
            => _data.LevelsStaticData.Length;
    }
}