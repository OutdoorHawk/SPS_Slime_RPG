using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using TMPro;
using UnityEngine;

namespace Project.Code.UI.Windows.PlayerHUD.Currency
{
    public class CurrencyContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsAmount;

        public void Init(PlayerCurrencyProgress currencyProgress)
        {
            _coinsAmount.text = currencyProgress.MoneyAmount.ToString();
        }
    }
}
