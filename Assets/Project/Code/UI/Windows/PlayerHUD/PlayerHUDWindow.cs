using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.StaticData.Units.Player;
using Project.Code.UI.Windows.PlayerHUD.Currency;
using Project.Code.UI.Windows.PlayerHUD.StatShop;
using UnityEngine;
using Zenject;

namespace Project.Code.UI.Windows.PlayerHUD
{
    public class PlayerHUDWindow : MonoBehaviour
    {
        [SerializeField] private StatsShopContainer _shopContainer;
        [SerializeField] private CurrencyContainer _currencyContainer;

        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private PlayerStatsProgress _statsProgress;
        private PlayerCurrencyProgress _currencyProgress;
        private ISaveLoadService _saveLoadService;
        private PlayerStaticData _playerData;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        private void Start()
        {
            InitFields();
            InitContainers();
            Subscribe();
        }

        private void InitFields()
        {
            _statsProgress = _progressService.Progress.PlayerStatsProgress;
            _currencyProgress = _progressService.Progress.PlayerCurrencyProgress;
            _playerData = _staticDataService.GetPlayerStaticData();
        }

        private void InitContainers()
        {
            _shopContainer.Init(_playerData, _statsProgress);
            _currencyContainer.Init(_currencyProgress);
        }

        private void Subscribe()
        {
            _shopContainer.SubscribeClicks();
            _shopContainer.OnUpgradeButtonPressed += HandleUpgradeButtonClicked;
        }

        private void UpdateHUD()
        {
            InitContainers();
        }

        private void HandleUpgradeButtonClicked(StatID id)
        {
            _currencyProgress.RemoveMoney(_statsProgress.GetStatProgress(id).StatUpgradeCost);
            float statValueIncrease = _playerData.GetStatValueIncrease(id);
            int statCostIncrease = _playerData.GetStatCostIncrease(id);
            _statsProgress.UpgradeStat(id, statValueIncrease, statCostIncrease);
            UpdateHUD();
            
            //_saveLoadService.SaveProgress(); TODO ACTIVATE LATER
        }

        private void OnDestroy()
        {
            _shopContainer.Cleanup();
        }
    }
}