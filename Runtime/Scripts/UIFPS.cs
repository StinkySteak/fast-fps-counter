using TMPro;
using UnityEngine;

namespace StinkySteak.FPSCounter
{
    public class UIFPS : MonoBehaviour
    {
        private static UIFPS Instance { get; set; }

        [SerializeField] private FPSCounterSystem _fpsCounterSystem;

        [SerializeField] private float _updateTextInterval = 0.1f;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private bool _isPersistent;

        private const int FPS_TEXT_BUFFER = 4;
        private readonly char[] _fpsTextBuffer = new char[FPS_TEXT_BUFFER];

        private float _nextUpdateTime;

        private void Awake()
        {
            SetupSingleton();
        }
        private void SetupSingleton()
        {
            if (!_isPersistent) return;

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }

            Destroy(gameObject);
        }

        private void LateUpdate()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (IsCooldown()) return;

            int fps = _fpsCounterSystem.GetFPS();

            for (int i = 0; i < FPS_TEXT_BUFFER; i++)
            {
                int digit = (fps % 10); // Extract the last digit

                _fpsTextBuffer[_fpsTextBuffer.Length - 1 - i] = (char)('0' + digit); // Convert digit to character

                fps /= 10;
            }

            // _text.SetText("{0}", fps);
            _text.SetCharArray(_fpsTextBuffer);
            SetCooldown();
        }

        private void SetCooldown()
            => _nextUpdateTime = Time.time + _updateTextInterval;

        private bool IsCooldown()
            => Time.time < _nextUpdateTime;
    }
}