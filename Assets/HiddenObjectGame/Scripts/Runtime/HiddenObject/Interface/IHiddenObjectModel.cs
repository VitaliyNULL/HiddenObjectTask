using R3;

namespace HiddenObjectGame.Runtime.HiddenObject.Interface
{
    public interface IHiddenObjectModel
    {
        public ReactiveProperty<bool> IsFounded { get; }
        public string Name { get; }
    }
}