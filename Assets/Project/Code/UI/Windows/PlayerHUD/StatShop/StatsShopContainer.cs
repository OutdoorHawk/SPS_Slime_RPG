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
            for (var i = 0; i < _shopItems.Count; i++) 
                _shopItems[i].Init(_statsProgress);
        }

        public void SubscribeClicks()
        {
            for (var i = 0; i < _shopItems.Count; i++) 
                _shopItems[i].OnUpgradeButtonPressed += NotifyUpgradeButtonClicked;
        } 
        
        public void Cleanup()
        {
            for (var i = 0; i < _shopItems.Count; i++) 
                _shopItems[i].OnUpgradeButtonPressed += NotifyUpgradeButtonClicked;
        }

        private void NotifyUpgradeButtonClicked(StatID id)
        {
            OnUpgradeButtonPressed?.Invoke(id);
        }
        
    }
}
