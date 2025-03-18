using System;
using UnityEngine;

namespace HiddenObjectGame.Runtime.Services
{
    public class VFXInstance : MonoBehaviour
    {
        private VFXObjectPool _objectPool;
        private Vector2 _pointToMove;
        private Transform _target;
        private Vector2 _startPoint;
        private Vector3 _controlPoint;

        [SerializeField] private float _controlPointOffset = 2f;
        [SerializeField] private float _duration = 5f;
        private float _timeElapsed = 0f;
        private event Action OnCompletedVFX;


        void Update()
        {
            _timeElapsed += Time.deltaTime;
            _pointToMove = _target.position;
            _controlPoint = GetControlPoint(_startPoint, _pointToMove, _controlPointOffset);

            float t = Mathf.Clamp01(_timeElapsed / _duration);
            Vector3 bezierPosition = GetQuadraticBezierPosition(t, _startPoint, _controlPoint, _pointToMove);
            transform.position = bezierPosition;
            if (t >= 1)
            {
                Deactivate();
            }
        }

        Vector3 GetControlPoint(Vector3 p0, Vector3 p2, float offset)
        {
            Vector3 midPoint = (p0 + p2) / 2;
            return new Vector3(midPoint.x, midPoint.y + offset, midPoint.z);
        }

        Vector3 GetQuadraticBezierPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
        }

        public void Initialize(VFXObjectPool objectPool)
        {
            _objectPool = objectPool;
        }


        public void Activate(Vector2 startPoint, Transform endPoint, Action onComplete)
        {
            _startPoint = startPoint;
            OnCompletedVFX = onComplete;
            _target = endPoint;
            transform.position = startPoint;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            OnCompletedVFX?.Invoke();
            OnCompletedVFX = null;
            gameObject.SetActive(false);
            _objectPool.Return(this);
        }
    }
}