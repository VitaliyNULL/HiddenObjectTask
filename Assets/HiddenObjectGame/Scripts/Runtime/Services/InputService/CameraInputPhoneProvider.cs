using HiddenObjectGame.Runtime.Services.InputService;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HiddenObjectGame.Runtime.Services
{
    public class CameraInputPhoneProvider : CameraInputBaseProvider
    {
        private Touch _touchZero;
        private Touch _touchOne;

        public CameraInputPhoneProvider(float cameraSpeed, float leftLimit, float rightLimit, float topLimit,
            float bottomLimit, float zoomMin, float zoomMax) : base(cameraSpeed, leftLimit, rightLimit, topLimit,
            bottomLimit, zoomMin, zoomMax)
        {
        }

        public override void ProcessInput()
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 2)
                {
                    _touchZero = Input.GetTouch(0);
                    _touchOne = Input.GetTouch(1);
                    if (EventSystem.current.IsPointerOverGameObject(_touchZero.fingerId) ||
                        EventSystem.current.IsPointerOverGameObject(_touchOne.fingerId))
                    {
                        return;
                    }

                    Zoom();
                }
                else
                {
                    Move();
                }
            }
        }

        protected override void Zoom()
        {
            Vector2 touchZeroPrevPos = _touchZero.position - _touchZero.deltaPosition;
            Vector2 touchOnePrevPos = _touchOne.position - _touchOne.deltaPosition;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (_touchZero.position - _touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            deltaMagnitudeDiff *= 0.01f;
            Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize + deltaMagnitudeDiff, ZoomMin, ZoomMax);
            Vector3 cameraPosition = Camera.transform.position;
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, LeftLimit, RightLimit);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, BottomLimit, TopLimit);
            Camera.transform.position = cameraPosition;
        }

        protected override void Move()
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
                var cameraPosition = Camera.transform.position;
                cameraPosition += direction * CameraSpeed * Time.deltaTime;
                cameraPosition =
                    new Vector3(Mathf.Clamp(cameraPosition.x, LeftLimit, RightLimit),
                        Mathf.Clamp(cameraPosition.y, BottomLimit, TopLimit),
                        cameraPosition.z);
                Camera.transform.position = cameraPosition;
            }
        }
    }
}