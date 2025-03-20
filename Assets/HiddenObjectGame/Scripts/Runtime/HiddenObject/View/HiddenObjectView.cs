using HiddenObjectGame.Runtime.HiddenObject.Interface;
using HiddenObjectGame.Runtime.HiddenObjectCollect;
using HiddenObjectGame.Runtime.Utilities;
using HiddenObjectGame.Runtime.VFX;
using R3;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObject.View
{
    public class HiddenObjectView : MonoBehaviour, IHiddenObjectView
    {
        private IHiddenObjectViewModel _viewModel;
        private readonly CompositeDisposable _compositeDisposable = new();
        private Camera _camera;
        [SerializeField] private string _uniqueID;
        public string GetID() => _uniqueID;

        private void OnValidate()
        {
            GuidGenerator.TryGenerateGuid(ref _uniqueID);
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        [Inject]
        private void Construct(IHiddenObjectViewModel viewModel, IHiddenObjectCollectModel collectModel,
            HiddenObjectCollectView hiddenObjectCollectView, VFXService vfxService)
        {
            _viewModel = viewModel;
            _viewModel.IsFounded
                .Subscribe((isFounded) => State(isFounded, collectModel, hiddenObjectCollectView, vfxService))
                .AddTo(_compositeDisposable);
        }

        private void State(bool isFounded, IHiddenObjectCollectModel collectModel,
            HiddenObjectCollectView hiddenObjectCollectView, VFXService vfxService)
        {
            if (isFounded)
            {
                collectModel.AddFoundedObject(_uniqueID);
                var type = _viewModel.GetObjectType();
                var startPos = transform.position;
                var hiddenObjectUI = hiddenObjectCollectView.GetHiddenObjectUI(_viewModel.GetObjectType());
                vfxService.SpawnVFX(type, startPos, hiddenObjectUI.GetTransform(),
                    () => hiddenObjectUI.CompleteObjectCollection()).Forget();
                Destroy(gameObject);
            }
        }

        public void OnClick()
        {
            _viewModel.OnClicked();
        }

        private void OnDestroy()
        {
            _compositeDisposable?.Dispose();
        }


        public HiddenObjectType GetObjectType() => _viewModel.GetObjectType();
    }
}