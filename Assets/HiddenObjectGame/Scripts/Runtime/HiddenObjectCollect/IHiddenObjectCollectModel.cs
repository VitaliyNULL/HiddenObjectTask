using ObservableCollections;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public interface IHiddenObjectCollectModel
    {
        public ObservableList<string> FoundedObjects { get; }
        public void AddFoundedObject(string id);
        
    }
}