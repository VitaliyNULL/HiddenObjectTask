using UnityEngine;

namespace HiddenObjectGame.Runtime.Services.InputService
{
    public abstract class CameraInputBaseProvider : ICameraInputProvider
    {
        protected Camera Camera { get; }
        protected float CameraSpeed { get; }
        protected bool MoveAllowed;
        protected Vector3 TouchPos;
        protected float LeftLimit;
        protected float RightLimit;
        protected float TopLimit;
        protected float BottomLimit;
        protected float ZoomMin;
        protected float ZoomMax;

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