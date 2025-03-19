using HiddenObjectGame.Runtime.HiddenObject;
using UnityEngine;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public interface IHiddenObjectEntityView
    {
        public HiddenObjectType HiddenObjectType { get; }
        public void SetData(IHiddenObjectCollectViewModel hiddenObjectCollectViewModel, Sprite sprite,
            HiddenObjectType type, int count);
        public void CompleteObjectCollection();
        public Transform GetTransform();
    }
}