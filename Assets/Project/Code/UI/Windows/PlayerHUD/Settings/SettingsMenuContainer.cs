using System;
using Project.Code.Infrastructure.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.Windows.PlayerHUD.Settings
{
    public class SettingsMenuContainer : MonoBehaviour
    {
        public event Action OnProgressDeleted;

        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _exitSettingsButton;
        [SerializeField] private Button _deleteProgressButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private Button _exitCreditsButton;
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _surePanel;
        [SerializeField] private GameObject _creditsPanel;
        [SerializeField] private Toggle _vibrationToggle;

        private void Awake()
        {
            _openSettingsButton.onClick.AddListener(EnableSettingsMenu);
            _exitSettingsButton.onClick.AddListener(DisableSettingsMenu);
            _deleteProgressButton.onClick.AddListener(OpenSurePanel);
            _noButton.onClick.AddListener(CloseSurePanel);
            _yesButton.onClick.AddListener(NotifyProgressDeleted);
            _creditsButton.onClick.AddListener(OpenCredits);
            _exitCreditsButton.onClick.AddListener(CloseCredits);
            _vibrationToggle.onValueChanged.AddListener(CheckVibrationSettings);
        }


        private void CloseCredits() => _creditsPanel.SetActive(false);

        private void OpenCredits() => _creditsPanel.SetActive(true);

        private void CloseSurePanel() => _surePanel.SetActive(false);

        private void OpenSurePanel() => _surePanel.SetActive(true);

        private void EnableSettingsMenu() => _mainPanel.SetActive(true);

        private void DisableSettingsMenu() => _mainPanel.SetActive(false);

        private void NotifyProgressDeleted() => OnProgressDeleted?.Invoke();

        private void CheckVibrationSettings(bool value) => PlayerPrefs.SetInt(Constants.VIBRATION, value ? 1 : 0);

        private void OnDestroy()
        {
            _openSettingsButton.onClick.RemoveListener(EnableSettingsMenu);
            _exitSettingsButton.onClick.RemoveListener(DisableSettingsMenu);
            _deleteProgressButton.onClick.RemoveListener(OpenSurePanel);
            _noButton.onClick.RemoveListener(CloseSurePanel);
            _yesButton.onClick.RemoveListener(NotifyProgressDeleted);
            _creditsButton.onClick.RemoveListener(OpenCredits);
            _exitCreditsButton.onClick.RemoveListener(CloseCredits);
            _vibrationToggle.onValueChanged.RemoveListener(CheckVibrationSettings);
        }
    }
}