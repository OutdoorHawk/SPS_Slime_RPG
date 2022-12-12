using DG.Tweening;
using UnityEngine;

namespace Project.Code.Runtime.Units.FloatingText
{
    [RequireComponent(typeof(TextMesh))]
    public class HitText : MonoBehaviour
    {
        [SerializeField] private TextMesh _textMesh;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _appearScaleTime = 0.2f;
        [SerializeField] private float _flyUpTime = 1f;
        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private float _flyUpStrength = 2f;

        private Sequence _textSequence;
        private Tween _scaleTween;
        private Tween _flyUpTween;
        private float _defaultScale;

        public void Init(float damage)
        {
            _textMesh.text = damage.ToString("0");
            _defaultScale = transform.localScale.magnitude;
            transform.localScale = Vector3.zero;
            EnableTextSequence();
        }

        private void EnableTextSequence()
        {
            _textSequence = DOTween.Sequence();
            Material textMat = _renderer.material;
            _textSequence.Append(_textMesh.transform.DOScale(_defaultScale, _appearScaleTime));
            _textSequence.Append(_textMesh.transform.DOBlendableMoveBy(Vector3.up * _flyUpStrength, _flyUpTime));
            _textSequence.Insert(_appearScaleTime, textMat.DOFade(0, _fadeTime))
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            _textSequence?.Kill();
        }
    }
}