using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using HiddenObjectGame.Runtime.HiddenObject;
using UnityEngine;

namespace HiddenObjectGame.Runtime.VFX
{
    public class VFXService : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<HiddenObjectType, VFXInstance> _vfxInstances;
        private readonly Dictionary<HiddenObjectType, VFXObjectPool> _objectPools = new();

        private void Awake()
        {
            foreach (var keyValuePair in _vfxInstances)
            {
                _objectPools.Add(keyValuePair.Key, new VFXObjectPool(keyValuePair.Value));
            }
        }

        public void SpawnVFX(HiddenObjectType type, Vector2 startPos, Transform target, Action onComplete = null)
        {
            _objectPools[type].Get().Activate(startPos, target, onComplete);
        }
    }
}