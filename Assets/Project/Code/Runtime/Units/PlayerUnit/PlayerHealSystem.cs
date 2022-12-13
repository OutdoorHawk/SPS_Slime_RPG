using System.Collections;
using Project.Code.Runtime.Units.Components;
using Project.Code.Runtime.Units.Components.Health;
using UnityEngine;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    public class PlayerHealSystem
    {
        private readonly HealthComponent _healthComponent;
        private readonly MonoBehaviour _mono;
        private float _loadedHprec;

        private const float HEAL_DELAY = 2;

        public PlayerHealSystem(PlayerSlime mono, HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
            _mono = mono;
            _mono.StartCoroutine(HPRecoveryRoutine());
        }

        public void UpdateStats(float loadedHprec)
        {
            _loadedHprec = loadedHprec;
        }

        private IEnumerator HPRecoveryRoutine()
        {
            do
            {
                if (_healthComponent != null) 
                    _healthComponent.TakeHeal(_loadedHprec);
                yield return new WaitForSeconds(HEAL_DELAY);
            } while (_mono != null);
        }
    }
}