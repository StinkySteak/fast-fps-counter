using UnityEngine;

namespace StinkySteak.FPSCounter
{
    public class FPSCounterSystem : MonoBehaviour
    {
        private const int BUFFER_SIZE = 1;
        private float[] _framesDeltaTimes;
        private int _indexer;

        private void Start()
        {
            _framesDeltaTimes = new float[BUFFER_SIZE];
        }

        private void Update()
        {
            if (_indexer >= BUFFER_SIZE)
                _indexer = 0;

            for (int i = 0; i < BUFFER_SIZE; i++)
            {
                _framesDeltaTimes[_indexer] = (1 / Time.deltaTime);
                _indexer++;
            }
        }

        public int GetFPS()
        {
            float fps = 0;

            for (int i = 0; i < _framesDeltaTimes.Length; i++)
            {
                fps += _framesDeltaTimes[i];
            }

            fps /= BUFFER_SIZE;

            return Mathf.RoundToInt(fps);
        }
    }
}