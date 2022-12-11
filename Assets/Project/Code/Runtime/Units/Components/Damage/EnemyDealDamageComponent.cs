namespace Project.Code.Runtime.Units.Components.Damage
{
    public class EnemyDealDamageComponent : DealDamageComponent
    {
        private HealthComponent _playerHealth;

        public void SetPlayer(HealthComponent playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void Attack()
        {
            base.Attack();
            _playerHealth.TakeDamage(_attackDetails);
        }
    }
}