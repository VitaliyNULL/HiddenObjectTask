using System;
using HiddenObjectGame.Runtime.HiddenObject.Interface;
using R3;

namespace HiddenObjectGame.Runtime.HiddenObject.Model
{
    public class HiddenObjectModel : IHiddenObjectModel
    {
        public ReactiveProperty<bool> IsFounded { get; } = new(false);
        public string Name { get; }

        public HiddenObjectModel(string name)
        {
            Name = name;
        }
    }
}