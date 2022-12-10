using SPS_Slime_RPG.Code.Infrastructure.Services.CoroutineRunner;
using SPS_Slime_RPG.Code.Infrastructure.StateMachine;
using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(this);
            //_gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}