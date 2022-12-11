using System;
using Project.Code.Infrastructure.Data;
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

        public void Init(PlayerStatsProgress statsProgress)
        {
            _statProgress = statsProgress.GetStatProgress(_statID);
            _statName.text = _statID.ToString();
            _statValue.text = SwitchTextFormat();
            _upgradeCost.text = _statProgress.StatUpgradeCost.ToString();
            _currentLevel.text = "Lv " + _statProgress.StatLvl;
            SwitchTextFormat();
        }

        private string SwitchTextFormat()
        {
            return _statID switch
            {
                StatID.ATK => _statProgress.StatValue.ToString("0"),
                StatID.ASPD => _statProgress.StatValue.ToString("0.00"),
                StatID.HP => _statProgress.StatValue.ToString("0"),
                StatID.HPREC => _statProgress.StatValue.ToString("0.0"),
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