using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Runtime.CustomData;
using Project.Code.Runtime.Units.Components;
using Project.Code.Runtime.Units.Components.Animation;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.Runtime.Units.FloatingText;
using Project.Code.StaticData.Units;
using Project.Code.UI.Units;
using UnityEngine;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    [SelectionBase]
    [RequireComponent(typeof(DealDamageComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitColorComponent))]
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] private HitText _floatingTextPrefab;
        [SerializeField] private HealthBar _healthBarPrefab;

        private HitColorComponent _hitColorComponent;
        protected HealthBar _healthBar;
        private RectTransform _hpPanel;

        public event Action<BaseUnit> OnUnitDead;
        protected PlayerProgress Progress { get; private set; }
        public HealthComponent HealthComponent { get; private set; }
        protected UnitStaticData UnitStaticData { get; private set; }

        public virtual void Init(UnitStaticData unitStaticData, PlayerProgress progress, RectTransform hpPanel)
        {
            _hpPanel = hpPanel;
            Progress = progress;
            UnitStaticData = unitStaticData;
            HealthComponent = GetComponent<HealthComponent>();
            _hitColorComponent = GetComponent<HitColorComponent>();
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
            HealthComponent.OnHeal += HandleHeal;
            HealthComponent.OnDeath += HandleDeath;
        }

        protected virtual void HandleDamageTaken(AttackDetails details)
        {
            _healthBar.UpdateHealth(HealthComponent.HealthPercent);
            _hitColorComponent.DoHitFlash();
            SpawnFloatingText(details);
        }

        private void SpawnFloatingText(AttackDetails details)
        {
            HitText text = Instantiate(_floatingTextPrefab, transform.position, Quaternion.identity);
            text.Init(details.Damage);
        }

        private void HandleHeal(float obj)
        {
            _healthBar.UpdateHealth(HealthComponent.HealthPercent);
        }

        protected virtual void HandleDeath()
        {
            Destroy(gameObject);
            OnUnitDead?.Invoke(this);
        }

        protected virtual void CleanUp()
        {
            HealthComponent.OnReceivedDamage -= HandleDamageTaken;
            HealthComponent.OnHeal -= HandleHeal;
            HealthComponent.OnDeath -= HandleDeath;
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}