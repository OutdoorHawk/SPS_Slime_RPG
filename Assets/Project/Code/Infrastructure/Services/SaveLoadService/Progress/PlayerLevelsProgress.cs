using System;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SaveLoadService.Progress
{
    [Serializable]
    public class PlayerLevelsProgress
    {
        [SerializeField] private int _currentLevel;

        public int CurrentLevel => _currentLevel;

        public void PassLevel() => _currentLevel++;
    }
}