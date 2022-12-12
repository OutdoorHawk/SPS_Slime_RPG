using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.Services.ZenjectFactory;
using Project.Code.Infrastructure.StaticData;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.UI
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly IStaticDataService _staticDataService;
        private readonly IZenjectFactory _zenjectFactory;

        public UIFactory(IStaticDataService staticDataService, IZenjectFactory zenjectFactory)
        {
            _staticDataService = staticDataService;
            _zenjectFactory = zenjectFactory;
        }
        
        public GameObject CreateWindow(WindowID id)
        {
            WindowConfig config = _staticDataService.GetWindow(id);
            return _zenjectFactory.Instantiate(config.WindowPrefab.gameObject, _uiRoot);
        }

        public void CreatePlayerHUD()
        {
            WindowConfig config = _staticDataService.GetWindow(WindowID.PlayerHUD);
            _zenjectFactory.Instantiate(config.WindowPrefab.gameObject, _uiRoot);
        }

        public void CreateUiRoot()
        {
            if (_uiRoot != null)
                Object.Destroy(_uiRoot.gameObject);
            WindowConfig config = _staticDataService.GetWindow(WindowID.UiRoot);
            GameObject uiRoot = Object.Instantiate(config.WindowPrefab.gameObject);
            Object.DontDestroyOnLoad(uiRoot);
            _uiRoot = uiRoot.transform;
        }

        public void ClearUIRoot()
        {
            for (int i = 0; i < _uiRoot.childCount; i++)
                Object.Destroy(_uiRoot.GetChild(i).gameObject);
        }
    }
}