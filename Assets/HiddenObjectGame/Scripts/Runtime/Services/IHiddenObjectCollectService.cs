using HiddenObjectGame.Runtime.HiddenObject;

namespace HiddenObjectGame.Runtime.Services
{
    public interface IHiddenObjectCollectService
    {
        public void AddFoundedObject(HiddenObjectType objectType);
    }
}