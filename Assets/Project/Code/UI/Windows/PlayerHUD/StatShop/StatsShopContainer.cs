using System;
using System.Collections.Generic;
using System.Linq;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress.Stats;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.StaticData.Units.Player;
using UnityEngine;

namespace Project.Code.UI.Windows.PlayerHUD.StatShop
{
    public class StatsShopContainer : MonoBehaviour
    {
        public event Action<StatID> OnUpgradeButtonPressed;
        
        [SerializeField] private Transform _shopItemsParent;

        private List<StatShopItem> _shopItems;
        private IStaticDataService _staticDataService;
        private PlayerStatsProgress _statsProgress;
        private PlayerStaticData _staticData;

        public void Init(PlayerStaticData playerData, PlayerStatsProgress statsProgress)
        {
            _statsProgress = statsProgress;
            _staticData = playerData;
            InitShopItems();
        }

        private void InitShopItems()
        {
            _shopItems = _shopItemsParent.GetComponentsInChildren<StatShopItem>().ToList();
            foreach (var item in _shopItems)
                item.Init(_statsProgress);
        }
        
        public void UpdateShopItems()
        {
            foreach (var item in _shopItems)
                item.UpdateItemInfo();
        }

        public void SubscribeClicks()
        {
            foreach (var item in _shopItems)
                item.OnUpgradeButtonPressed += NotifyUpgradeButtonClicked;
        }

        public void CheckStatCosts(int moneyAmount)
        {
            foreach (var item in _shopItems)
                item.CheckEnoughMoney(moneyAmount);
        }

        public void Cleanup()
        {
            foreach (var item in _shopItems)
                item.OnUpgradeButtonPressed += NotifyUpgradeButtonClicked;
        }

        private void NotifyUpgradeButtonClicked(StatID id)
        {
            OnUpgradeButtonPressed?.Invoke(id);
        }
    }
}
