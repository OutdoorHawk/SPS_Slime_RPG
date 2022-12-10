using System.Collections.Generic;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.AssetProvider;
using Project.Code.Infrastructure.StaticData;
using Project.Code.Runtime.Player;
using Project.Code.StaticData;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<WindowID, WindowConfig> _windows = new();
        private GameObject[] _roads;
        private PlayerSlime _playerSlime;
        private PlayerStaticData _playerStaticData;

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
            LoadPlayer();
            LoadWorld();
        }

        private void LoadWindows()
        {
            foreach (var window in _data.Windows)
                _windows.Add(window.ID, window);
        }

        private void LoadPlayer()
        {
            _playerSlime = _data.Player;
            _playerStaticData = _data.PlayerStaticData;
        }

        private void LoadWorld()
        {
            _roads = _data.Roads;
        }

        public WindowConfig GetWindow(WindowID id) => 
            _windows.TryGetValue(id, out var windowConfig) ? windowConfig : null;

        public PlayerSlime GetPlayerPrefab() 
            => _playerSlime;

        public PlayerStaticData GetPlayerStaticData() 
            => _playerStaticData;

        public GameObject[] GetRoads() 
            => _roads;
    }
}