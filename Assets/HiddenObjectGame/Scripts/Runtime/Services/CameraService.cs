using Unity.Cinemachine;
using UnityEngine;

namespace HiddenObjectGame.Runtime.Services
{
    public class CameraService : MonoBehaviour
    {
        public CinemachineCamera virtualCamera; // Ссилка на вашу Cinemachine Virtual Camera
        public float moveSpeed = 1f; // Швидкість переміщення камери
        public float zoomSpeed = 1f; // Швидкість зуму
        private float targetOrthographicSize;
        private Vector3 lastTouchPosition;

        private CinemachineConfiner2D confiner; // Для доступу до Cinemachine Confiner 2D

        void Start()
        {
            // Отримуємо компонент CinemachineConfiner 2D
            confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner == null)
            {
                Debug.LogError("Cinemachine Confiner 2D не знайдений!");
            }

            // Отримуємо початковий розмір зуму
            targetOrthographicSize = virtualCamera.Lens.OrthographicSize;
        }

        void Update()
        {
            // Рух камери для мобільного пристрою (слайд пальцем)
            HandleTouchMovement();

            // Рух камери для ПК (переміщення миші)
            HandleMouseMovement();

            // Зум камери для мобільного пристрою (двома пальцями)
            HandlePinchZoom();

            // Зум камери для ПК (колесо миші)
            HandleMouseZoom();

            // Обмежуємо зум в межах Cinemachine Confiner 2D
            RestrictZoomWithConfiner();
        }

        void HandleTouchMovement()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 delta = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0) * moveSpeed *
                                    Time.deltaTime;
                    virtualCamera.transform.Translate(-delta, Space.World);
                }
            }
        }

        void HandleMouseMovement()
        {
            if (Input.GetMouseButton(0)) // Ліва кнопка миші
            {
                Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * moveSpeed *
                                     Time.deltaTime;
                virtualCamera.transform.Translate(-mouseDelta, Space.World);
            }
        }

        void HandlePinchZoom()
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                float previousTouchDeltaMag = (touch0.position - touch1.position).magnitude;
                float currentTouchDeltaMag = (touch0.position - touch1.position).magnitude;

                float deltaMagnitudeDiff = previousTouchDeltaMag - currentTouchDeltaMag;
                targetOrthographicSize += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;

                // Оновлюємо зум камери
                virtualCamera.Lens.OrthographicSize =
                    Mathf.Clamp(targetOrthographicSize, 3f, 10f); // Задайте межі зуму
            }
        }

        void HandleMouseZoom()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                targetOrthographicSize -= scrollInput * zoomSpeed * 10f; // змінюйте швидкість за потреби
                virtualCamera.Lens.OrthographicSize =
                    Mathf.Clamp(targetOrthographicSize, 3f, 10f); // Задайте межі зуму
            }
        }

        void RestrictZoomWithConfiner()
        {
            Collider2D confinerCollider = confiner.BoundingShape2D;
            Bounds bounds = confinerCollider.bounds;
            float maxZoomOut = Mathf.Min(bounds.size.x / 2, bounds.size.y / 2);
            virtualCamera.Lens.OrthographicSize =
                Mathf.Clamp(virtualCamera.Lens.OrthographicSize, 3f, maxZoomOut);
        }
    }
}