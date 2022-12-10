using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.StaticData;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.UI
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly IStaticDataService _staticDataService;

        public UIFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
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