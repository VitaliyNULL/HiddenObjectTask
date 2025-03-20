using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BGGames.Runtime.Loading.Operations
{
    public class LoadAddressablesOperation : BaseLoadingOperation
    {
        private readonly AssetLabelReference _downloadLabel;

        public LoadAddressablesOperation(AssetLabelReference downloadLabel)
        {
            Description = "Initializing assets...";
            _downloadLabel = downloadLabel;
        }

        public override async UniTask Load(Action<float> onProgress)
        {
            var downloadSizeAsync = Addressables.GetDownloadSizeAsync(_downloadLabel);
            await downloadSizeAsync;
            float downloadMegabytesSize = downloadSizeAsync.Result / Mathf.Pow(1024, 2);
            var loadAssetsAsync = Addressables.LoadAssetsAsync<object>(_downloadLabel, null);
            if (downloadSizeAsync.Result > 0)
            {
                while (!loadAssetsAsync.IsDone)
                {
                    onProgress?.Invoke(loadAssetsAsync.PercentComplete);
                    float downloadedMegabytes =
                        loadAssetsAsync.GetDownloadStatus().DownloadedBytes / Mathf.Pow(1024, 2);
                    Description = $"Downloaded megabytes {downloadedMegabytes:0.00}/{downloadMegabytesSize:0.00} MB";
                    await UniTask.Yield();
                }
            }
            else
            {
                onProgress?.Invoke(FullProgress / 2);
                await loadAssetsAsync;
                onProgress?.Invoke(FullProgress);
            }
            Addressables.Release(downloadSizeAsync);
            Addressables.Release(loadAssetsAsync);
        }
    }
}