using System;
using ObservableCollections;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    [Serializable]
    public class HiddenObjectCollectModel : IHiddenObjectCollectModel
    {
        public ObservableList<string> FoundedObjects { get; set; } = new();
        public void AddFoundedObject(string id)
        {
            FoundedObjects.Add(id);
        }
        
    }
}