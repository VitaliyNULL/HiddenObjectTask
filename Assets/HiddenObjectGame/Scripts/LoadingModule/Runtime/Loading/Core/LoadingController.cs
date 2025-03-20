using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces;

namespace HiddenObjectGame.LoadingModule.Runtime.Loading.Core
{
    public class LoadingController : ILoadingController
    {
        public event Action OnStartLoading;
        public event Action OnFinishLoading;
        public event Action<string, float> OnProgress;

        public async UniTaskVoid Load(Queue<ILoadingOperation> operations)
        {
            int operationsCount = operations.Count;
            OnStartLoading?.Invoke();
            float totalProgress = 0;
            int completedOperations = 0;
            foreach (var operation in operations)
            {
                await operation.Load(progress =>
                {
                    float operationWeight = 1f / operationsCount;
                    float currentProgress = (completedOperations + progress) * operationWeight;
                    OnProgress?.Invoke(operation.Description, currentProgress);
                });
                completedOperations++;
                totalProgress = (float)completedOperations / operationsCount;
                OnProgress?.Invoke(operation.Description, totalProgress);
            }
            
            await UniTask.WaitForSeconds(1f);
            OnFinishLoading?.Invoke();
        }
    }
}