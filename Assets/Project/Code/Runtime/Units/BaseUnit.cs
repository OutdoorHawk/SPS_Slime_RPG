using System;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.UpdateBehavior;
using Project.Code.Runtime.CustomData;
using Project.Code.Runtime.Units.Components.Animation;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.Runtime.Units.Components.Health;
using Project.Code.Runtime.Units.FloatingText;
using Project.Code.StaticData.Units;
using Project.Code.UI.Units;
using UnityEngine;
using Zenject;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    [SelectionBase]
    [RequireComponent(typeof(DealDamageComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitColorComponent))]
    public class BaseUnit : MonoBehaviour
    {
        public event Action<BaseUnit> OnUnitDead;

        [SerializeField] private HitText _floatingTextPrefab;
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private GameObject _deadParticlesPrefab;

        private HitColorComponent _hitColorComponent;
        private RectTransform _hpPanel;
        private IUpdateBehaviourService _updateBehaviourService;

        public HealthComponent HealthComponent { get; private set; }
        protected HealthBar HPBar { get; private set; }
        protected PlayerProgress Progress { get; private set; }

        private const float FLOATING_TEXT_OFFSET_RATE = 0.5f;

        [Inject]
        private void Construct(IUpdateBehaviourService updateBehaviourService)
        {
            _updateBehaviourService = updateBehaviourService;
        }

        protected virtual void Init(UnitStaticData unitStaticData, PlayerProgress progress, RectTransform hpPanel)
        {
            _hpPanel = hpPanel;
            Progress = progress;
            HealthComponent = GetComponent<HealthComponent>();
            _hitColorComponent = GetComponent<HitColorComponent>();
            InitHealthBar();
            Subscribe();
        }

        private void InitHealthBar()
        {
            HPBar = Instantiate(_healthBarPrefab);
            HPBar.Init(_updateBehaviourService);
            HPBar.SetTargetToFollow(transform, _hpPanel);
        }

        private void OnEnable() => 
            _updateBehaviourService.UpdateEvent += Tick;

        private void OnDisable() => 
            _updateBehaviourService.UpdateEvent -= Tick;

        protected virtual void Subscribe()
        {
            HealthComponent.OnReceivedDamage += HandleDamageTaken;
            HealthComponent.OnHeal += HandleHeal;
            HealthComponent.OnDeath += HandleDeath;
        }

        protected virtual void CleanUp()
        {
            HealthComponent.OnReceivedDamage -= HandleDamageTaken;
            HealthComponent.OnHeal -= HandleHeal;
            HealthComponent.OnDeath -= HandleDeath;
        }

        protected virtual void HandleDamageTaken(AttackDetails details)
        {
            HPBar.UpdateHealth(HealthComponent.HealthPercent);
            _hitColorComponent.DoHitFlash();
            HPBar.UpdateHealthText(HealthComponent.CurrentHealth);
            SpawnFloatingText(details);
        }

        private void HandleHeal(float obj)
        {
            HPBar.UpdateHealth(HealthComponent.HealthPercent);
        }

        protected virtual void HandleDeath()
        {
            GameObject particles = Instantiate(_deadParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 1.5f);
            Destroy(gameObject);
            OnUnitDead?.Invoke(this);
        }

        private void SpawnFloatingText(AttackDetails details)
        {
            HitText text = Instantiate(_floatingTextPrefab,
                transform.position + Vector3.up * FLOATING_TEXT_OFFSET_RATE,
                Quaternion.identity);
            text.Init(details.Damage);
        }

        protected virtual void Tick()
        {
        }

        private void OnDestroy() 
            => CleanUp();
    }
}