using System.Collections.Generic;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public interface IHiddenObjectCollectModel
    {
        public List<string> FoundedObjects { get; }
        public void Save();
        public void Load();
        public void AddFoundedObject(string id);
    }

    
}