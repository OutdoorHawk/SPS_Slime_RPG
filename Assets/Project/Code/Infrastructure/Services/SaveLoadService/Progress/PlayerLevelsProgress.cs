using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerLevelsProgress
    {
        public event Action OnFightPassed;
        public event Action OnLevelPassed;


        [SerializeField] private int _currentLevel;
        [SerializeField] private int _currentFight;
        [SerializeField] private int _maxFightsOnLevel;

        public PlayerLevelsProgress()
        {
            _currentLevel = 0;
            _currentFight = 0;
            _maxFightsOnLevel = 4;
        }

        public int CurrentLevel => _currentLevel;
        public int CurrentFight => _currentFight;
        public int MaxFightsOnLevel => _maxFightsOnLevel;

        public void PassLevel()
        {
            _currentLevel++;
            _currentFight = 0;
            _maxFightsOnLevel = Random.Range(4, 6);
            OnLevelPassed?.Invoke();
        }

        public void PassFight()
        {
            if (_currentFight == _maxFightsOnLevel)
                PassLevel();
            else
            {
                _currentFight++;
                OnFightPassed?.Invoke();
            }
        }
    }
}