using System;
using HiddenObjectGame.Runtime.HiddenObject.Interface;
using R3;
using UnityEngine;

namespace HiddenObjectGame.Runtime.HiddenObject.ViewModel
{
    public class HiddenObjectViewModel : IHiddenObjectViewModel, IDisposable
    {
        private readonly IHiddenObjectModel _model;
        private readonly CompositeDisposable _disposable = new();

        public HiddenObjectViewModel(IHiddenObjectModel model)
        {
            _model = model;
            _model.IsFounded.Subscribe(OnFounded).AddTo(_disposable);
        }

        private void OnFounded(bool isFounded)
        {
            if (isFounded)
            {
                Debug.Log($"Founded object with name {_model.Name}");
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void OnClicked()
        {
            _model.IsFounded.Value = true;
        }
    }
}