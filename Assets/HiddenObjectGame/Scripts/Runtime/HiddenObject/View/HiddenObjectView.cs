using System;
using HiddenObjectGame.Runtime.HiddenObject.Interface;
using HiddenObjectGame.Runtime.Services;
using R3;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObject.View
{
    public class HiddenObjectView : MonoBehaviour, IHiddenObjectView
    {
        private IHiddenObjectViewModel _viewModel;
        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        private void Construct(IHiddenObjectViewModel viewModel, HiddenObjectSaveData saveData,
            HiddenObjectCollectView hiddenObjectCollectView, VFXService vfxService)
        {
            _viewModel = viewModel;
            _viewModel.IsFounded.Subscribe((isFounded) =>
            {
                if (isFounded)
                {
                    hiddenObjectCollectView.GetHiddenObjectUIPos(_viewModel.GetObjectType());
                    saveData.AddFoundedObject(gameObject.GetInstanceID());
                    Destroy(gameObject);
                }
            }).AddTo(_compositeDisposable);
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