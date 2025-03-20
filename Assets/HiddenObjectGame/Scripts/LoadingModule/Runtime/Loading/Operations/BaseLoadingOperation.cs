using System;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces;

namespace HiddenObjectGame.LoadingModule.Runtime.Loading.Operations
{
    public abstract class BaseLoadingOperation: ILoadingOperation
    {
        protected readonly float FullProgress = 1f;

        public string Description { get; protected set; }
        
        public abstract UniTask Load(Action<float> onProgress);
    }
}