using R3;

namespace HiddenObjectGame.Runtime.HiddenObject.Interface
{
    public interface IHiddenObjectViewModel
    {
        public ReactiveProperty<bool> IsFounded { get; }
        public void OnClicked();
        public HiddenObjectType GetObjectType();
    }
}