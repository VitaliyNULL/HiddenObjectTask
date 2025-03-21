using System;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces;
using UnityEngine;

namespace HiddenObjectGame.LoadingModule.Runtime.Loading.Operations
{
    public class LoggerLoadingOperation : LoadingOperationDecorator
    {
        public LoggerLoadingOperation(ILoadingOperation loadingOperation) : base(loadingOperation)
        {
        }

        public override async UniTask Load(Action<float> onProgress)
        {
            Debug.Log($"{InnerLoadingOperation.GetType().Name} started: ");
            await base.Load(onProgress);
            Debug.Log($"{InnerLoadingOperation.GetType().Name} finished: ");
            ;
        }
    }
}