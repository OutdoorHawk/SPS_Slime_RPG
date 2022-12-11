namespace Project.Code.Runtime.Units.Components
{
    public class EnemyDealDamageComponent : DealDamageComponent
    {
        private HealthComponent _playerHealth;

        public void SetPlayer(HealthComponent playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void AttackPlayer()
        {
            base.AttackPlayer();
            _playerHealth.TakeDamage(_attackDetails);
        }
    }
}