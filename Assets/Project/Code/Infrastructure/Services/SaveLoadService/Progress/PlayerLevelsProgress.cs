using System;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerLevelsProgress
    {
        public event Action OnFightPassed;

        [SerializeField] private int _currentLevel;
        [SerializeField] private int _currentFight;

        public PlayerLevelsProgress()
        {
            _currentLevel = 0;
            _currentFight = 0;
        }

        public int CurrentLevel => _currentLevel;
        public int CurrentFight => _currentFight;

        public void PassLevel()
        {
            _currentLevel++;
            _currentFight = 0;
        }

        public void PassFight()
        {
            _currentFight++;
            OnFightPassed?.Invoke();
        }
        
        public void ResetFights() 
            => _currentFight = 0;
    }
}