using UnityEngine;
using UnityEngine.EventSystems;

namespace HiddenObjectGame.Runtime.Services
{
    public abstract class CameraInputBaseProvider : ICameraInputProvider
    {
        protected Camera Camera { get; }
        protected float CameraSpeed { get; }
        protected bool MoveAllowed;
        protected Vector3 TouchPos;
        protected readonly float LeftLimit;
        protected readonly float RightLimit;
        protected readonly float TopLimit;
        protected readonly float BottomLimit;
        protected readonly float ZoomMin;
        protected readonly float ZoomMax;

        protected CameraInputBaseProvider(float cameraSpeed, float leftLimit, float rightLimit, float topLimit,
            float bottomLimit, float zoomMin, float zoomMax)
        {
            Camera = Camera.main;
            CameraSpeed = cameraSpeed;
            LeftLimit = leftLimit;
            RightLimit = rightLimit;
            TopLimit = topLimit;
            BottomLimit = bottomLimit;
            ZoomMin = zoomMin;
            ZoomMax = zoomMax;
        }

        public abstract void ProcessInput();

        protected abstract void Zoom();

        protected abstract void Move();
    }
}