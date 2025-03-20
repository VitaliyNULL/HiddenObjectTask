using System;
using System.Threading;
using BGGames.Runtime.Infrastructure.Core;
using BGGames.Runtime.Infrastructure.Interfaces;
using Cysharp.Threading.Tasks;

namespace BGGames.Runtime.Loading.Operations
{
    public class LoadSceneOperation : BaseLoadingOperation
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly SceneType _sceneType;
        private CancellationTokenSource _cancellationTokenSource;

        public LoadSceneOperation(ISceneLoader sceneLoader, SceneType sceneType) : base()
        {
            Description = "Loading scene...";
            _sceneLoader = sceneLoader;
            _sceneType = sceneType;
        }

        private void RecreateCancellationTokenSource()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public override async UniTask Load(Action<float> onProgress)
        {
            RecreateCancellationTokenSource();
            var task = _sceneLoader.LoadScene(_sceneType);
            while (!task.Status.IsCompleted())
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                    onProgress?.Invoke(_sceneLoader.Progress);
                }

                await UniTask.Yield(_cancellationTokenSource.Token);
            }
            onProgress?.Invoke(FullProgress);
        }
    }
}