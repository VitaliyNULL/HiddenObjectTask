using UnityEngine;
using UnityEngine.EventSystems;

namespace HiddenObjectGame.Runtime.InputService
{
    public class CameraInputPCProvider : CameraInputBaseProvider
    {
        private float _scrollValue;
        private readonly float _zoomSpeed = 40f;

        private readonly float _initialZoom;
        private readonly float _initialLeftLimit;
        private readonly float _initialRightLimit;
        private readonly float _initialTopLimit;
        private readonly float _initialBottomLimit;

        public CameraInputPCProvider(float cameraSpeed, float leftLimit, float rightLimit, float topLimit,
            float bottomLimit, float zoomMin, float zoomMax) : base(cameraSpeed, leftLimit, rightLimit, topLimit,
            bottomLimit, zoomMin, zoomMax)
        {
            _initialZoom = Camera.orthographicSize;
            _initialLeftLimit = leftLimit;
            _initialRightLimit = rightLimit;
            _initialTopLimit = topLimit;
            _initialBottomLimit = bottomLimit;
        }

        public override void ProcessInput()
        {
            _scrollValue = Input.GetAxis("Mouse ScrollWheel");

            if (Mathf.Abs(_scrollValue) > 0.001f)
            {
                Zoom();
            }
            else
            {
                Move();
            }
        }

        protected override void Zoom()
        {
            Vector3 mouseWorldPosBeforeZoom = Camera.ScreenToWorldPoint(Input.mousePosition);
            float newSize = Mathf.Clamp(Camera.orthographicSize - _scrollValue * _zoomSpeed, ZoomMin, ZoomMax);
            float zoomRatio = _initialZoom / newSize;
            Camera.orthographicSize = newSize;
            LeftLimit = _initialLeftLimit * zoomRatio;
            RightLimit = _initialRightLimit * zoomRatio;
            TopLimit = _initialTopLimit * zoomRatio;
            BottomLimit = _initialBottomLimit * zoomRatio;
            Vector3 mouseWorldPosAfterZoom = Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = mouseWorldPosBeforeZoom - mouseWorldPosAfterZoom;
            Camera.transform.position += offset;
            Vector3 cameraPosition = Camera.transform.position;
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, LeftLimit, RightLimit);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, BottomLimit, TopLimit);
            Camera.transform.position = cameraPosition;
        }


        protected override void Move()
        {
            if (Input.touchSupported && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    MoveAllowed = !EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                    TouchPos = Camera.ScreenToWorldPoint(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved && MoveAllowed)
                {
                    Vector3 direction = TouchPos - Camera.ScreenToWorldPoint(touch.position);
                    MoveCamera(direction);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                MoveAllowed = !EventSystem.current.IsPointerOverGameObject();
                TouchPos = Camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0) && MoveAllowed)
            {
                Vector3 direction = TouchPos - Camera.ScreenToWorldPoint(Input.mousePosition);
                MoveCamera(direction);
            }
        }

        private void MoveCamera(Vector3 direction)
        {
            var cameraPosition = Camera.transform.position;
            cameraPosition += direction * CameraSpeed * Time.deltaTime;
            cameraPosition = new Vector3(
                Mathf.Clamp(cameraPosition.x, LeftLimit, RightLimit),
                Mathf.Clamp(cameraPosition.y, BottomLimit, TopLimit),
                cameraPosition.z);
            Camera.transform.position = cameraPosition;
        }
    }
}