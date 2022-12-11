using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using TMPro;
using UnityEngine;

namespace Project.Code.UI.Windows.PlayerHUD.Currency
{
    public class CurrencyContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsAmount;
        private PlayerCurrencyProgress _currencyProgress;

        public void Init(PlayerCurrencyProgress currencyProgress)
        {
            _currencyProgress = currencyProgress;
            UpdateMoney();
        }

        public void UpdateMoney()
        {
            _coinsAmount.text = _currencyProgress.MoneyAmount.ToString();
        }
    }
}
