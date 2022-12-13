using System;
using Project.Code.Extensions;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.StateMachine;
using Project.Code.Infrastructure.StateMachine.States;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.StaticData.Units.Player;
using Project.Code.UI.Windows.PlayerHUD.Currency;
using Project.Code.UI.Windows.PlayerHUD.LevelProgress;
using Project.Code.UI.Windows.PlayerHUD.Settings;
using Project.Code.UI.Windows.PlayerHUD.StatShop;
using UnityEngine;
using Zenject;

namespace Project.Code.UI.Windows.PlayerHUD
{
    public class PlayerHUDWindow : MonoBehaviour
    {
        [SerializeField] private StatsShopContainer _shopContainer;
        [SerializeField] private CurrencyContainer _currencyContainer;
        [SerializeField] private LevelProgressContainer _levelProgressContainer;
        [SerializeField] private SettingsMenuContainer _settingsMenuContainer;

        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private PlayerStatsProgress _statsProgress;
        private PlayerCurrencyProgress _currencyProgress;
        private ISaveLoadService _saveLoadService;
        private PlayerStaticData _playerData;
        private ISceneContextService _sceneContextService;
        private PlayerSlime _playerSlime;
        private PlayerLevelsProgress _levelsProgress;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ISceneContextService sceneContextService, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _sceneContextService = sceneContextService;
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
            _levelsProgress = _progressService.Progress.PlayerLevelsProgress;
            _playerData = _staticDataService.GetPlayerStaticData();
            _playerSlime = _sceneContextService.Player;
        }

        private void InitContainers()
        {
            _shopContainer.Init(_playerData, _statsProgress,_currencyProgress);
            _currencyContainer.Init(_currencyProgress);
            CheckStatCosts();
            _levelProgressContainer.Init(_levelsProgress,_staticDataService);
        }

        private void Subscribe()
        {
            _shopContainer.SubscribeClicks();
            _shopContainer.OnUpgradeButtonPressed += HandleUpgradeButtonClicked;
            _currencyProgress.OnMoneyChanged += CheckStatCosts;
            _levelsProgress.OnFightPassed += _levelProgressContainer.UpdateFightProgress;
            _settingsMenuContainer.OnProgressDeleted += ResetProgress;
        }

        public void EnableBossTitle()
        {
            _levelProgressContainer.EnableBossTitle();
        }
        
        public void EnableDefeatTitle(Action onDone)
        {
            _levelProgressContainer.EnableDefeatTitle(onDone);
        }

        private void CheckStatCosts()
        {
            _currencyContainer.UpdateMoney();
            _shopContainer.CheckStatCosts();
        }

        private void HandleUpgradeButtonClicked(StatID id)
        {
            UpdatePlayerProgress(id);
            UpdatePlayerView();
            _saveLoadService.SaveProgress(); //TODO ACTIVATE LATER
        }

        private void UpdatePlayerProgress(StatID id)
        {
            _currencyProgress.RemoveMoney(_statsProgress.GetStatProgress(id).StatUpgradeCost);
            float statValueIncrease = _playerData.GetStatValueIncrease(id);
            int statCostIncrease = _playerData.GetStatCostIncrease(id);
            _statsProgress.UpgradeStat(id, statValueIncrease, statCostIncrease);
        }

        private void UpdatePlayerView()
        {
            _shopContainer.UpdateShopItems();
            _currencyContainer.UpdateMoney();
            _playerSlime.UpdateComponents();
        }

        private void ResetProgress()
        {
            var progress = new PlayerProgress();
            Utils.LoadDefaultValuesToProgress(progress, _staticDataService);
            _progressService.Progress = progress;
            _saveLoadService.SaveProgress();
            _gameStateMachine.Enter<LoadLevelState>();
        }

        private void OnDestroy()
        {
            _shopContainer.OnUpgradeButtonPressed -= HandleUpgradeButtonClicked;
            _currencyProgress.OnMoneyChanged -= CheckStatCosts;
            _levelsProgress.OnFightPassed -= _levelProgressContainer.UpdateFightProgress;
            _settingsMenuContainer.OnProgressDeleted -= ResetProgress;
            _shopContainer.Cleanup();
        }
    }
}