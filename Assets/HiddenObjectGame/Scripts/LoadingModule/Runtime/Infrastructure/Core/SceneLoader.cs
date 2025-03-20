using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Interfaces;
using UnityEngine.AddressableAssets;

namespace HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Core
{
    public class SceneLoader : ISceneLoader
    {
        public float Progress { get; private set; }
        private readonly SceneConfig _sceneConfig;
        private CancellationTokenSource _cancellationTokenSource;

        public SceneLoader(SceneConfig sceneConfig)
        {
            _sceneConfig = sceneConfig;
        }

        private void RecreateCancellationTokenSource()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async UniTask LoadScene(SceneType sceneType)
        {
            RecreateCancellationTokenSource();
            if (_sceneConfig.TryGetScene(sceneType, out AssetReference scene))
            {
                var loadSceneAsync = Addressables.LoadSceneAsync(scene);
                while (!loadSceneAsync.IsDone)
                {
                    if (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        Progress = loadSceneAsync.PercentComplete;
                    }

                    await UniTask.Yield(_cancellationTokenSource.Token);
                }
            }
            else
            {
                throw new Exception($"Scene {sceneType} not found in SceneConfig");
            }
        }
    }
}