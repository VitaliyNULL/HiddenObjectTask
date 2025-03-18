using System;
using UnityEngine;

namespace HiddenObjectGame.Runtime.Services
{
    public class VFXInstance : MonoBehaviour
    {
        private ObjectPool<VFXInstance> _objectPool;
        private Vector2 _pointToMove;
        private Vector2 _startPoint;
        private Vector3 _controlPoint;

        [SerializeField] private float _controlPointOffset = 2f;
        [SerializeField] private float _duration = 5f;
        private float _timeElapsed = 0f;

        void Update()
        {
            _timeElapsed += Time.deltaTime;
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

        public void Initialize(ObjectPool<VFXInstance> objectPool)
        {
            _objectPool = objectPool;
        }


        public void Activate(Vector2 startPoint, Vector2 endPoint)
        {
            _startPoint = startPoint;
            transform.position = startPoint;
            _pointToMove = endPoint;
            _controlPoint = GetControlPoint(_startPoint, _pointToMove, _controlPointOffset);
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _objectPool.Return(this);
        }
    }
}