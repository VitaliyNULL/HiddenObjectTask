using System;
using AYellowpaper.SerializedCollections;
using HiddenObjectGame.Runtime.HiddenObject;
using UnityEngine;

namespace HiddenObjectGame.Runtime.Services
{
    public class VFXService : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<HiddenObjectType, VFXInstance> _vfxInstances;
        private SerializedDictionary<HiddenObjectType, ObjectPool<VFXInstance>> _objectPools;

        private void Awake()
        {
            foreach (var keyValuePair in _vfxInstances)
            {
                _objectPools.Add(keyValuePair.Key, new ObjectPool<VFXInstance>(keyValuePair.Value));
            }
        }

        public void SpawnVFX(HiddenObjectType type, Vector2 startPos, Vector2 endPos)
        {
            _objectPools[type].Get().Activate(startPos, endPos);
        }
    }
}