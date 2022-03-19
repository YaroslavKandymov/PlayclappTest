using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ValuesInputFields : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _spawnTime;
        [SerializeField] private TMP_InputField _distance;
        [SerializeField] private TMP_InputField _speed;

        public event Action<string> SpawnTimeChanged;
        public event Action<string> DistanceChanged;
        public event Action<string> SpeedChanged;

        private void OnEnable()
        {
            _spawnTime.onEndEdit.AddListener(OnSpawnTimeChanged);
            _distance.onEndEdit.AddListener(OnDistanceChanged);
            _speed.onEndEdit.AddListener(OnSpeedChanged);
        }

        private void OnDisable()
        {
            _spawnTime.onEndEdit.RemoveListener(OnSpawnTimeChanged);
            _distance.onEndEdit.RemoveListener(OnDistanceChanged);
            _speed.onEndEdit.RemoveListener(OnSpeedChanged);
        }

        private void OnSpawnTimeChanged(string value)
        {
           SpawnTimeChanged?.Invoke(value);
        }

        private void OnDistanceChanged(string value)
        {
            DistanceChanged?.Invoke(value);
        }

        private void OnSpeedChanged(string value)
        {
            SpeedChanged?.Invoke(value);
        }
    }
}
