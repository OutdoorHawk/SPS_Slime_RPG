using Project.Code.Infrastructure.StateMachine;
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
            //_gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }
}