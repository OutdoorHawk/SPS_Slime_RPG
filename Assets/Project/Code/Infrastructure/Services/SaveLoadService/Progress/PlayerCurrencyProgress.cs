using System;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerCurrencyProgress
    {
        [SerializeField] private int _moneyAmount;
        
        public int MoneyAmount => _moneyAmount;

        public void AddMoney(int amount)
        {
            _moneyAmount += amount;
        } 
        
        public void RemoveMoney(int amount)
        {
            if (_moneyAmount - amount >= 0) 
                _moneyAmount -= amount;
        }
    }
}