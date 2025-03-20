using System;
using BGGames.Runtime.Loading.Interfaces;
using Cysharp.Threading.Tasks;

namespace BGGames.Runtime.Loading.Operations
{
    public abstract class BaseLoadingOperation: ILoadingOperation
    {
        protected readonly float FullProgress = 1f;

        public string Description { get; protected set; }
        
        public abstract UniTask Load(Action<float> onProgress);
    }
}