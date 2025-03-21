using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public class HiddenObjectEntityViewFactory : PlaceholderFactory<Vector3, Quaternion,
        Transform, UniTask<HiddenObjectEntityView>>
    {
        public class Factory : IFactory<Vector3, Quaternion,
            Transform, UniTask<HiddenObjectEntityView>>, IValidatable
        {
            private readonly DiContainer _container;
            private readonly AssetReference _assetReference;
            private GameObject _prefab;
            private HiddenObjectSpriteContainer _hiddenObjectSpriteContainer;
            private CancellationTokenSource _cancellationToken;

            public Factory(DiContainer container, AssetReference assetReference)
            {
                _container = container;
                _assetReference = assetReference;
            }

            public async UniTask<HiddenObjectEntityView> Create(Vector3 pos, Quaternion quaternion,
                Transform parent = null)
            {
                RecreateCancellationToken();
                await InitializePrefab().AttachExternalCancellation(_cancellationToken.Token);
                if (_cancellationToken.Token.IsCancellationRequested) return null;
                HiddenObjectEntityView instance = null;
                if (parent == null)
                {
                    instance = _container.InstantiatePrefabForComponent<HiddenObjectEntityView>(_prefab, pos,
                        quaternion, null);
                }
                else
                {
                    instance = _container.InstantiatePrefabForComponent<HiddenObjectEntityView>(_prefab, parent);
                }

                return instance;
            }

            private async UniTask InitializePrefab()
            {
                if (_prefab == null)
                {
                    var loadOp = Addressables.LoadAssetAsync<GameObject>(_assetReference);
                    await loadOp.ToUniTask().AttachExternalCancellation(_cancellationToken.Token);
                    if (_cancellationToken.Token.IsCancellationRequested) return;
                    _prefab = loadOp.Result;
                    Addressables.Release(loadOp);
                }
            }

            private void RecreateCancellationToken()
            {
                _cancellationToken?.Cancel();
                _cancellationToken?.Dispose();
                _cancellationToken = new CancellationTokenSource();
            }

            public async void Validate()
            {
                Debug.Log("Validate");
                RecreateCancellationToken();
                await InitializePrefab().AttachExternalCancellation(_cancellationToken.Token);
                _container.InstantiatePrefab(_prefab);
            }
        }
    }
}