using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HiddenObjectGame.Runtime.Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private float _zoomMin;
        [SerializeField] private float _zoomMax;
        [SerializeField] private float _leftLimit;
        [SerializeField] private float _rightLimit;
        [SerializeField] private float _topLimit;
        [SerializeField] private float _bottomLimit;

        private ICameraInputProvider _cameraInputProvider;

        private void Awake()
        {
            _cameraInputProvider = new CameraInputPCProvider(_cameraSpeed, _leftLimit, _rightLimit, _topLimit,
                _bottomLimit, _zoomMin, _zoomMax);
        }


        private void Update()
        {
            _cameraInputProvider.ProcessInput();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var x = _rightLimit - Mathf.Abs(_leftLimit);
            var y = _topLimit - Mathf.Abs(_bottomLimit);

            Gizmos.DrawWireCube(new Vector3(x, y), new Vector3(_rightLimit - _leftLimit, _topLimit - _bottomLimit));
        }
    }
}