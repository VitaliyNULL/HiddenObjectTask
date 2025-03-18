using HiddenObjectGame.Runtime.HiddenObject.Interface;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HiddenObjectGame.Runtime.Services
{
    public class HiddenObjectInputService : IHiddenObjectInputService, ITickable
    {
        private readonly Camera _camera;
        private readonly LayerMask _targetLayer;

        public HiddenObjectInputService(LayerMask layerMask)
        {
            _targetLayer = layerMask;
            _camera = Camera.main;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _targetLayer))
                {
                    TryInteractWithHiddenObject(hit);
                }
            }
        }

        private void TryInteractWithHiddenObject(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out IHiddenObjectView view))
            {
                view.OnClick();
            }
        }
    }
}