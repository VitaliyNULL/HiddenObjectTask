using System;
using HiddenObjectGame.Runtime.HiddenObject.Interface;
using R3;
using UnityEngine;

namespace HiddenObjectGame.Runtime.HiddenObject.ViewModel
{
    public class HiddenObjectViewModel : IHiddenObjectViewModel, IDisposable
    {
        private readonly IHiddenObjectModel _model;

        public HiddenObjectViewModel(IHiddenObjectModel model)
        {
            _model = model;
            _model.IsFounded.Subscribe(OnFounded);
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
        }

        public void OnClicked()
        {
            _model.IsFounded.Value = true;
        }
    }
}