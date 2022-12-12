using System;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.Windows.PlayerHUD.StatShop
{
    public class StatShopItem : MonoBehaviour
    {
        public event Action<StatID> OnUpgradeButtonPressed;

        [SerializeField] private StatID _statID;
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statValue;
        [SerializeField] private TMP_Text _upgradeCost;
        [SerializeField] private TMP_Text _currentLevel;
        [SerializeField] private Button _upgradeButton;
        
        private StatProgress _statProgress;
        private PlayerCurrencyProgress _currencyProgress;

        public void Init(PlayerStatsProgress statsProgress, PlayerCurrencyProgress playerCurrencyProgress)
        {
            _currencyProgress = playerCurrencyProgress;
            _statProgress = statsProgress.GetStatProgress(_statID);
            UpdateItemInfo();
        }

        public void UpdateItemInfo()
        {
            _statName.text = _statID.ToString();
            _statValue.text = SwitchValueFormat();
            _currentLevel.text = "Lv " + _statProgress.StatLvl;
            CheckEnoughMoney();
        }

        public void CheckEnoughMoney()
        {
            if (_statProgress.StatUpgradeCost > _currencyProgress.MoneyAmount)
                SetItemInactive();
            else
                SetItemActive();
        }

        private void SetItemActive()
        {
            _upgradeButton.interactable = true;
            _upgradeCost.text = $"<sprite index=300> {_statProgress.StatUpgradeCost.ToString()}";
        }

        private void SetItemInactive()
        {
            _upgradeButton.interactable = false;
            _upgradeCost.text = $"<sprite index=300> <color=red>{_statProgress.StatUpgradeCost.ToString()}";
        }

        private string SwitchValueFormat()
        {
            return _statID switch
            {
                StatID.ATK => _statProgress.StatValue.ToString("0"),
                StatID.ASPD => _statProgress.StatValue.ToString("0.00"),
                StatID.HP => _statProgress.StatValue.ToString("0"),
                StatID.HPREC => _statProgress.StatValue.ToString("0.0"),
                StatID.CRIT => $"{_statProgress.StatValue:0}%",
                _ => null
            };
        }

        private void OnEnable() =>
            _upgradeButton.onClick.AddListener(NotifyButtonClicked);

        private void OnDisable() =>
            _upgradeButton.onClick.RemoveListener(NotifyButtonClicked);

        private void NotifyButtonClicked()
        {
            OnUpgradeButtonPressed?.Invoke(_statID);
        }
    }
}