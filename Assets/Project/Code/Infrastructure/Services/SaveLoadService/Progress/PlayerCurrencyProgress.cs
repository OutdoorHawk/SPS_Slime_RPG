using System;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerCurrencyProgress
    {
        public event Action OnMoneyChanged;
        
        [SerializeField] private int _moneyAmount;
        
        public int MoneyAmount => _moneyAmount;

        public void AddMoney(int amount)
        {
            _moneyAmount += amount;
            OnMoneyChanged?.Invoke();
        } 
        
        public void RemoveMoney(int amount)
        {
            if (_moneyAmount - amount >= 0) 
                _moneyAmount -= amount;
            OnMoneyChanged?.Invoke();
        }
    }
}