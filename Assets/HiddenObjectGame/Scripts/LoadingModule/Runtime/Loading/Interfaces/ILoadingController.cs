using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces
{
    public interface ILoadingController
    {
        public event Action OnStartLoading;
        public event Action OnFinishLoading;
        public event Action<string, float> OnProgress;

        public UniTaskVoid Load(Queue<ILoadingOperation> operations);
    }
}