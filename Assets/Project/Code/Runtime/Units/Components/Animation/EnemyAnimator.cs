using UnityEngine;

namespace Project.Code.Runtime.Units.Components.Animation
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        
        private readonly int Walk = Animator.StringToHash("Walk");
        private readonly int Attack = Animator.StringToHash("Attack");

        private const float MIN_MOVE_VALUE = 0.1f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UpdatePlayerAnim(float speed)
        {
            if (speed > MIN_MOVE_VALUE) 
                EnableWalk();
            else
                DisableWalk();
        }

        public void DoAttack()
        {
            _animator.SetTrigger(Attack);
        }

        private void EnableWalk()
        {
            _animator.SetBool(Walk, true);
        }

        private void DisableWalk()
        {
            _animator.SetBool(Walk, false);
        }
    }
}
