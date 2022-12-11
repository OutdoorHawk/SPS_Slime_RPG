using Project.Code.Runtime.CustomData;
using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.Runtime.Units.Components.Damage
{
    public abstract class DealDamageComponent : MonoBehaviour
    {
        protected AttackDetails _attackDetails;
        protected float _attackSpeed;

        private float _countDown = 0;

        public void Init(UnitStaticData staticData)
        {
            _attackDetails = new AttackDetails(staticData.DamageAmount);
            _attackSpeed = staticData.AttackSpeed;
        }

        public virtual void UpdateAttack()
        {
            if (_countDown <= 0)
                Attack();

            _countDown -= Time.deltaTime;
        }

        protected virtual void Attack()
        {
            ResetCountdown();
        }

        private void ResetCountdown()
        {
            _countDown = 1 / _attackSpeed;
        }
    }
}