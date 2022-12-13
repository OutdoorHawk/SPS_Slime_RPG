using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.BG
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Image _bgImage;
        [SerializeField] private float _speed = 1;

        private Material _cloudsMaterial;

        private void Awake()
        {
            _cloudsMaterial = _bgImage.material;
        }

        public void UpdateParallax()
        {
            Vector2 currentOffset = _cloudsMaterial.mainTextureOffset;
            _cloudsMaterial.mainTextureOffset = new Vector2(currentOffset.x + Time.deltaTime * _speed, 0);
        }

        private void OnDestroy()
        {
            _cloudsMaterial.mainTextureOffset = Vector2.zero;
        }
    }
}