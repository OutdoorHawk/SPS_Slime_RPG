using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Runtime.CustomData;
using Project.Code.Runtime.Units.Components;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.StaticData.Units;
using Project.Code.UI.Units;
using UnityEngine;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    [SelectionBase]
    [RequireComponent(typeof(DealDamageComponent))]
    [RequireComponent(typeof(HealthComponent))]
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarPrefab;

        private HealthBar _healthBar;
        private RectTransform _hpPanel;

        public event Action<BaseUnit> OnUnitDead;
        protected PlayerProgress Progress { get; private set; }
        public HealthComponent HealthComponent { get; private set; }
        protected UnitStaticData UnitStaticData { get; private set; }
        protected DealDamageComponent DealDamageComponent { get; private set; }

        public virtual void Init(UnitStaticData unitStaticData, PlayerProgress progress, RectTransform hpPanel)
        {
            _hpPanel = hpPanel;
            Progress = progress;
            UnitStaticData = unitStaticData;
            DealDamageComponent = GetComponent<DealDamageComponent>();
            HealthComponent = GetComponent<HealthComponent>();
            InitHealthBar();
            Subscribe();
        }

        private void InitHealthBar()
        {
            _healthBar = Instantiate(_healthBarPrefab);
            _healthBar.Init();
            _healthBar.SetTargetToFollow(transform, _hpPanel);
        }

        protected virtual void Subscribe()
        {
            HealthComponent.OnReceivedDamage += HandleDamageTaken;
            HealthComponent.OnDeath += HandleDeath;
        }

        protected virtual void HandleDamageTaken(AttackDetails details)
        {
        }

        protected virtual void HandleDeath()
        {
            Destroy(gameObject);
            OnUnitDead?.Invoke(this);
        }

        protected virtual void CleanUp()
        {
            HealthComponent.OnReceivedDamage -= HandleDamageTaken;
            HealthComponent.OnDeath -= HandleDeath;
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}