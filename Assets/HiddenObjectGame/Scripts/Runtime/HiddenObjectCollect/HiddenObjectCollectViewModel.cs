using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObjectGame.Runtime.HiddenObject;
using HiddenObjectGame.Runtime.HiddenObject.View;
using R3;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public class HiddenObjectCollectViewModel : MonoBehaviour, IHiddenObjectCollectViewModel
    {
        [SerializeField] private List<HiddenObjectView> _hiddenObjectViews = new();
        public Dictionary<HiddenObjectType, int> NeedToFoundObjects { get; } = new();
        public ReactiveProperty<bool> Initialized { get; } = new(false);
        public event Action<HiddenObjectType, int> OnFoundedObject;
        public ReactiveProperty<bool> OnAllFounded { get; } = new(false);
        private IHiddenObjectCollectModel _collectModel;

        [Inject]
        private void Construct(IHiddenObjectCollectModel collectModel)
        {
            _collectModel = collectModel;
        }

        private void Awake()
        {
            DestroyFoundedObjects();
            foreach (var hiddenObjectView in _hiddenObjectViews)
            {
                HiddenObjectType key = hiddenObjectView.GetObjectType();
                NeedToFoundObjects.TryAdd(key, 0);
                NeedToFoundObjects[key]++;
            }

            foreach (var needToFoundObject in NeedToFoundObjects)
            {
                Debug.Log(needToFoundObject.Key + " " + needToFoundObject.Value);
            }

            Initialized.Value = true;

            if (NeedToFoundObjects.Count == 0)
            {
                OnAllFounded.Value = true;
            }
        }


        public void AddFoundedObject(HiddenObjectType objectType)
        {
            var val = --NeedToFoundObjects[objectType];
            NeedToFoundObjects[objectType] = val;
            if (val <= 0)
            {
                NeedToFoundObjects.Remove(objectType);
                if (NeedToFoundObjects.Count == 0)
                {
                    OnAllFounded.Value = true;
                }
            }

            OnFoundedObject?.Invoke(objectType, val);
        }

        private void DestroyFoundedObjects()
        {
            var list = new List<HiddenObjectView>(_hiddenObjectViews);
            foreach (var hiddenObjectView in _hiddenObjectViews)
            {
                var foundedObject = _collectModel.FoundedObjects.Any(id =>
                {
                    if (id == hiddenObjectView.GetID())
                    {
                        return true;
                    }

                    return false;
                });
                if (foundedObject)
                {
                    list.Remove(hiddenObjectView);
                    Destroy(hiddenObjectView.gameObject);
                }
            }

            _hiddenObjectViews = list;
        }
    }
}