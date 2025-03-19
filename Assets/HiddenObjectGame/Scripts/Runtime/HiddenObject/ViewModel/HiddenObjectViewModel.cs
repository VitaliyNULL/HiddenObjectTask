using System;
using HiddenObjectGame.Runtime.HiddenObject.Interface;
using HiddenObjectGame.Runtime.HiddenObjectCollect;
using R3;

namespace HiddenObjectGame.Runtime.HiddenObject.ViewModel
{
    public class HiddenObjectViewModel : IHiddenObjectViewModel, IDisposable
    {
        private readonly IHiddenObjectModel _model;
        private readonly CompositeDisposable _disposable = new();

        public ReactiveProperty<bool> IsFounded { get; } =
            new(false);

        public HiddenObjectViewModel(IHiddenObjectModel model, IHiddenObjectCollectViewModel collectViewModel)
        {
            _model = model;
            _model.IsFounded.Subscribe((isFounded) =>
            {
                if (isFounded)
                {
                    IsFounded.Value = true;
                    collectViewModel.AddFoundedObject(_model.ObjectType);
                }
            }).AddTo(_disposable);
        }


        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void OnClicked()
        {
            _model.IsFounded.Value = true;
        }

        public HiddenObjectType GetObjectType() => _model.ObjectType;
    }
}