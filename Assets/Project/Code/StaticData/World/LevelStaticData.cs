using UnityEngine;

namespace Project.Code.StaticData.World
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Static Data/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private float _damageIncrease = 10f;
        [SerializeField] private float _healthIncrease = 50;
        [SerializeField] private int _moneyIncrease = 5;

        public float DamageIncrease => _damageIncrease;
        public float HealthIncrease => _healthIncrease;
        public int MoneyIncrease => _moneyIncrease;

        public void ResetToDefaultValues()
        {
            _damageIncrease = 10;
            _healthIncrease = 50;
            _moneyIncrease = 5;
        }

        public void IncreaseValue(float configStatMultiplier)
        {
            _damageIncrease *= configStatMultiplier;
            _healthIncrease *= configStatMultiplier;
            _moneyIncrease = (int)(configStatMultiplier * _moneyIncrease);
        }
    }
}