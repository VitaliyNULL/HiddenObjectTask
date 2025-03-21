using System;
using System.Collections.Generic;
using HiddenObjectGame.Runtime.HiddenObject;
using R3;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public interface IHiddenObjectCollectViewModel
    {
        public event Action<HiddenObjectType,int> OnFoundedObject;

        public ReactiveProperty<bool> Initialized { get; }
        public Dictionary<HiddenObjectType, int> NeedToFoundObjects { get; }
        public void AddFoundedObject(HiddenObjectType objectType);
    }
}