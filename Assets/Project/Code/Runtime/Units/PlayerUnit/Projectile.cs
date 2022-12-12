using System;
using DG.Tweening;
using Project.Code.Runtime.Units.EnemyUnit;
using UnityEngine;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Enemy> OnHit;

        [SerializeField] private GameObject _hitParticles;
        [SerializeField] private float _projectileSpeed = 5;
        [SerializeField] private float _jumpPowerRate = 0.5f;
        [SerializeField] private float _jumpTimeRate = 0.5f;
        [SerializeField] private float _scaleTime = 0.5f;

        private Transform _model;
        private Tween _projectileTween;
        private Tween _scaleTween;
        private Vector3 _defaultLocalPosition;
        private Enemy _currentTarget;
        private float _jumpTime;
        private float _jumpPower = 1;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _model = transform.GetChild(0).transform;
            _defaultLocalPosition = _model.localPosition;
            _defaultScale = _model.localScale;
            _model.localScale = Vector3.zero;
        }

        private void StartMovingUp()
        {
            _jumpTime = Vector3.Distance(transform.position, _currentTarget.transform.position) / _projectileSpeed;
            _jumpTime *= _jumpTimeRate;
            _jumpPower = _jumpTime * _jumpPowerRate;
            _projectileTween = _model.DOLocalMove(Vector3.up * _jumpPower, _jumpTime).OnComplete(StartMovingDown)
                .SetEase(Ease.OutCubic);
        }

        private void StartMovingDown()
        {
            _projectileTween?.Kill();
            _jumpTime = Vector3.Distance(transform.position, _currentTarget.transform.position) / _projectileSpeed;
            _projectileTween = _model.DOLocalMove(_defaultLocalPosition, _jumpTime).SetEase(Ease.InCubic);
        }

        public void SeekTarget(Enemy currentTarget, Action<Enemy> onHit)
        {
            _currentTarget = currentTarget;
            OnHit = onHit;
            StartMovingUp();
            EnableScaleTween();
        }

        private void EnableScaleTween()
        {
            _scaleTween = _model.DOScale(_defaultScale, _scaleTime);
        }

        private void Update()
        {
            if (_currentTarget == null)
            {
                Destroy(gameObject);
                return;
            }

            FlyToTarget();
        }

        private void FlyToTarget()
        {
            Vector3 direction = _currentTarget.transform.position - transform.position;

            float distanceThisFrame = _projectileSpeed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }

        private void HitTarget()
        {
            OnHit?.Invoke(_currentTarget);
            GameObject particles = Instantiate(_hitParticles, transform.position, transform.rotation);
            Destroy(particles, 1.5f);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _projectileTween?.Kill();
            _scaleTween?.Kill();
        }
    }
}