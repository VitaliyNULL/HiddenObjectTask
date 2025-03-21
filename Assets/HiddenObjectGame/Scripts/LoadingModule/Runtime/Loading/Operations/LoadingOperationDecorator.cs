using System;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces;

namespace HiddenObjectGame.LoadingModule.Runtime.Loading.Operations
{
    public class LoadingOperationDecorator : BaseLoadingOperation
    {
        protected readonly ILoadingOperation InnerLoadingOperation;

        protected LoadingOperationDecorator(ILoadingOperation loadingOperation)
        {
            InnerLoadingOperation = loadingOperation;
        }

        public override async UniTask Load(Action<float> onProgress)
        {
            await InnerLoadingOperation.Load(onProgress);
        }
    }
}