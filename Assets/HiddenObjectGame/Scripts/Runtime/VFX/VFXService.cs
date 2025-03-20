using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.Runtime.HiddenObject;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HiddenObjectGame.Runtime.VFX
{
    public class VFXService : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<HiddenObjectType, AssetReference> _vfxReferences;
        private readonly Dictionary<HiddenObjectType, VFXObjectPool> _objectPools = new();

        private void Awake()
        {
            foreach (var keyValuePair in _vfxReferences)
            {
                _objectPools.Add(keyValuePair.Key, new VFXObjectPool(keyValuePair.Value));
            }
        }

        public async UniTaskVoid SpawnVFX(HiddenObjectType type, Vector2 startPos, Transform target,
            Action onComplete = null)
        {
            var result = await _objectPools[type].Get();
            result.Activate(startPos, target, onComplete);
        }
    }
}