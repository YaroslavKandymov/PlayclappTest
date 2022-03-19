using System;
using System.Collections;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private ValuesInputFields _valuesInputFields;
        [SerializeField] private Vector3 _startPosition;

        private float _moveTime = 1;
        private int _spawnTime = 1;
        private CubeMover _cubeMover;
        private float _distance = 10;
        private WaitForSeconds _seconds;

        private void Awake()
        {
            _cubeMover = new CubeMover(_cube.transform);

            _seconds = new WaitForSeconds(_spawnTime);
        }

        private void OnEnable()
        {
            _cubeMover.Complete += OnComplete;

            _valuesInputFields.SpawnTimeChanged += OnSpawnTimeChanged;
            _valuesInputFields.DistanceChanged += OnDistanceChanged;
            _valuesInputFields.SpeedChanged += OnSpeedChanged;
        }

        private void OnDisable()
        {
            _cubeMover.Complete -= OnComplete;

            _valuesInputFields.SpawnTimeChanged -= OnSpawnTimeChanged;
            _valuesInputFields.DistanceChanged -= OnDistanceChanged;
            _valuesInputFields.SpeedChanged -= OnSpeedChanged;
        }

        private void Start()
        {
            Move();
        }

        private void OnComplete()
        {
            _cube.gameObject.SetActive(false);
            _cube.transform.position = _startPosition;

            StartCoroutine(SpawnCoroutine());
        }

        private void Move()
        {
            var finalPosition = Vector3.right * _distance;
            _cubeMover.Move(finalPosition, _moveTime);
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return _seconds;

            _cube.gameObject.SetActive(true);
            Move();
        }

        private void OnSpawnTimeChanged(string value)
        {
            _spawnTime = TryParse(value);

            if (_spawnTime < 0)
                throw new InvalidOperationException($"Wrong value: {value}");
            
            _seconds = new WaitForSeconds(_spawnTime);
        }

        private void OnDistanceChanged(string value)
        {
            _distance = TryParse(value);
        }

        private void OnSpeedChanged(string value)
        {
            var speed = TryParse(value);

            _moveTime = 1f / speed;
        }

        private int TryParse(string value)
        {
            if (int.TryParse(value, out int number) == false)
                throw new InvalidOperationException($"Wrong value: {value}");

            return number;
        }
    }
}
