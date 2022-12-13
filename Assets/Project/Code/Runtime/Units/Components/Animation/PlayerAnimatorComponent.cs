using DG.Tweening;
using UnityEngine;

namespace Project.Code.Runtime.Units.Components.Animation
{
    public class PlayerAnimatorComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradeParticles;
        [SerializeField] private ParticleSystem _movingParticles;
        [SerializeField] private Transform _slimeModel;
        [SerializeField] private float _walkingScaleMax = 1.25f;
        [SerializeField] private float _walkingScaleMin = 0.75f;
        [SerializeField] private float _idleScaleMax;
        [SerializeField] private float _idleScaleMin;
        [SerializeField] private float _idleTime;
        [SerializeField] private float _walkingTime;

        private GameObject _upgradeParticlesGO;
        private Sequence _idleSeq;
        private Sequence _walkingSeq;
        private Sequence _resetSeq;

        private float _defaultZScale;
        private float _defaultYScale;
        private float _defaultXScale;

        private const float RESET_TIME = 0.25f;

        private void Awake()
        {
            CollectScales();
            _upgradeParticlesGO = Instantiate(_upgradeParticles, transform.position, Quaternion.identity);
        }

        private void CollectScales()
        {
            _defaultZScale = _slimeModel.localScale.z;
            _defaultYScale = _slimeModel.localScale.y;
            _defaultXScale = _slimeModel.localScale.x;
        }

        public void EnableIdleAnim()
        {
            ResetSequences();
            PrepareToIdle();
            _movingParticles.Stop();
        }

        public void EnableWalkAnim()
        {
            ResetSequences();
            PrepareToWalking();
            _movingParticles.Play();
        }

        public void SpawnUpgradeParticles()
        {
            _upgradeParticlesGO.gameObject.SetActive(true);
        }

        private void PrepareToIdle()
        {
            ResetSequences();
            _resetSeq = DOTween.Sequence();
            _resetSeq.Append(_slimeModel.DOScaleZ(_idleScaleMin, RESET_TIME));
            _resetSeq.Insert(0, _slimeModel.DOScaleY(_idleScaleMax, RESET_TIME));
            _resetSeq.Insert(0, _slimeModel.DOScaleX(_idleScaleMax, RESET_TIME));
            _resetSeq.OnComplete(StartIdleSequence);
        }

        private void StartIdleSequence()
        {
            _idleSeq = DOTween.Sequence();
            _idleSeq.Append(_slimeModel.DOScaleZ(_idleScaleMax, _idleTime)).SetEase(Ease.Linear);
            _idleSeq.Append(_slimeModel.DOScaleZ(_idleScaleMin, _idleTime)).SetEase(Ease.Linear);
            _idleSeq.Insert(0, _slimeModel.DOScaleY(_idleScaleMin, _idleTime))
                .SetEase(Ease.Linear);
            _idleSeq.Insert(_idleTime, _slimeModel.DOScaleY(_idleScaleMax, _idleTime))
                .SetEase(Ease.Linear);
            _idleSeq.Insert(0, _slimeModel.DOScaleX(_idleScaleMin, _idleTime))
                .SetEase(Ease.Linear);
            _idleSeq.Insert(_idleTime, _slimeModel.DOScaleX(_idleScaleMax, _idleTime))
                .SetEase(Ease.Linear);
            _idleSeq.SetLoops(-1);
        }

        private void PrepareToWalking()
        {
            ResetSequences();
            _resetSeq = DOTween.Sequence();
            _resetSeq.Append(_slimeModel.DOScaleZ(_walkingScaleMin, RESET_TIME));
            _resetSeq.Insert(0, _slimeModel.DOScaleY(_walkingScaleMax, RESET_TIME));
            _resetSeq.Insert(0, _slimeModel.DOScaleX(_walkingScaleMax, RESET_TIME));
            _resetSeq.OnComplete(StartWalkingSequence);
        }

        private void StartWalkingSequence()
        {
            _walkingSeq = DOTween.Sequence();
            _walkingSeq.Append(_slimeModel.DOScaleZ(_walkingScaleMax, _walkingTime)).SetEase(Ease.Linear);
            _walkingSeq.Append(_slimeModel.DOScaleZ(_walkingScaleMin, _walkingTime)).SetEase(Ease.Linear);

            _walkingSeq.Insert(0, _slimeModel.DOScaleX(_walkingScaleMin, _walkingTime)).SetEase(Ease.Linear);
            _walkingSeq.Insert(_walkingTime, _slimeModel.DOScaleX(_walkingScaleMax, _walkingTime))
                .SetEase(Ease.Linear);

            _walkingSeq.Insert(0, _slimeModel.DOScaleY(_walkingScaleMin, _walkingTime)).SetEase(Ease.Linear);
            _walkingSeq.Insert(_walkingTime, _slimeModel.DOScaleY(_walkingScaleMax, _walkingTime))
                .SetEase(Ease.Linear);


            _walkingSeq.SetLoops(-1);
        }

        private void ResetSequences()
        {
            _walkingSeq?.Kill();
            _idleSeq?.Kill();
            _resetSeq?.Kill();
        }

        private void OnDestroy()
        {
            ResetSequences();
        }
    }
}