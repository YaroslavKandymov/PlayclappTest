using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class CubeMover
    {
        private readonly Transform _transform;

        public event Action Complete;

        public CubeMover(Transform transform)
        {
            _transform = transform;
        }

        public void Move(Vector3 position, float time)
        {
            if(time < 0)
                throw new InvalidOperationException("Wrong time");

            _transform.DOMove(position, time).SetEase(Ease.Linear).OnComplete(() => Complete?.Invoke());
        }
    }
}
