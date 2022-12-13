using Project.Code.Infrastructure.StateMachine;
using Project.Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            Application.targetFrameRate = 90;
            _gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }
}