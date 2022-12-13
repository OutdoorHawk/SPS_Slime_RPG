using TMPro;
using UnityEngine;

namespace Project.Code.Test
{
    public class FPSCounter : MonoBehaviour
    {
        private TMP_Text _text;
        private float _fps;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _fps = 1.0f / Time.deltaTime;
            _text.text = "FPS: " + (int)_fps;
        }
    }
}