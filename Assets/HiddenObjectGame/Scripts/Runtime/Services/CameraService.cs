using System;
using Unity.Cinemachine;
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
        private Camera _camera;
        private bool _moveAllowed;
        private Vector3 _touchPos;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);
                    if (EventSystem.current.IsPointerOverGameObject(touchZero.fingerId) ||
                        EventSystem.current.IsPointerOverGameObject(touchOne.fingerId))
                    {
                        return;
                    }

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    Zoom(deltaMagnitudeDiff * 0.01f);
                }
                else
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                        {
                            _moveAllowed = false;
                        }
                        else
                        {
                            _moveAllowed = true;
                        }

                        _touchPos = _camera.ScreenToWorldPoint(touch.position);
                    }
                    else if (touch.phase == TouchPhase.Moved && _moveAllowed)
                    {
                        Vector3 direction = _touchPos - _camera.ScreenToWorldPoint(touch.position);
                        var cameraPosition = _camera.transform.position;
                        cameraPosition += direction * _cameraSpeed * Time.deltaTime;
                        cameraPosition =
                            new Vector3(Mathf.Clamp(cameraPosition.x, _leftLimit, _rightLimit),
                                Mathf.Clamp(cameraPosition.y, _bottomLimit, _topLimit),
                                cameraPosition.z);
                        _camera.transform.position = cameraPosition;
                    }
                }
                
            }
        }


        private void Zoom(float increment)
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + increment,
                _zoomMin, _zoomMax);
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