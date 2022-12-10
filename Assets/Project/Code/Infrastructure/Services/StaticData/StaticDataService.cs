using System.Collections.Generic;
using SPS_Slime_RPG.Code.Infrastructure.Data;
using SPS_Slime_RPG.Code.Infrastructure.Services.AssetProvider;
using SPS_Slime_RPG.Code.Infrastructure.StaticData;
using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<WindowID, WindowConfig> _windows = new();

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
        }

        public WindowConfig GetWindow(WindowID id)
        {
            return _windows.TryGetValue(id, out var windowConfig) ? windowConfig : null;
        }

        private void LoadWindows()
        {
            foreach (var window in _data.Windows)
                _windows.Add(window.ID, window);
        }
    }
}