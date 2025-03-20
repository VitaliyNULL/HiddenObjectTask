using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObjectGame.Runtime.HiddenObject;
using R3;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public class HiddenObjectCollectPresenter : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private HiddenObjectEntityView _hiddenObjectEntityViewPrefab;
        [SerializeField] private HiddenObjectSpriteContainer _hiddenObjectSpriteContainer;
        private IHiddenObjectCollectViewModel _hiddenObjectCollectViewModel;
        private List<IHiddenObjectEntityView> _hiddenObjectUIs = new List<IHiddenObjectEntityView>();

        [Inject]
        private void Construct(IHiddenObjectCollectViewModel hiddenObjectCollectViewModel)
        {
            _hiddenObjectCollectViewModel = hiddenObjectCollectViewModel;
            hiddenObjectCollectViewModel.Initialized.Subscribe(OnInitialized);
        }


        public IHiddenObjectEntityView GetHiddenObjectUI(HiddenObjectType type)
        {
            var hiddenObjectUI = _hiddenObjectUIs.FirstOrDefault(x => x.HiddenObjectType == type);
            if (hiddenObjectUI == null)
            {
                return null;
            }

            return hiddenObjectUI;
        }

        private void OnInitialized(bool obj)
        {
            foreach (var keyValuePair in _hiddenObjectCollectViewModel.NeedToFoundObjects)
            {
                IHiddenObjectEntityView spawnedObject = Instantiate(_hiddenObjectEntityViewPrefab, _content);
                if (_hiddenObjectSpriteContainer.TryGetSprite(keyValuePair.Key, out Sprite sprite))
                {
                    spawnedObject.SetData(_hiddenObjectCollectViewModel, sprite, keyValuePair.Key, keyValuePair.Value);
                    _hiddenObjectUIs.Add(spawnedObject);
                }
                else
                {
                    throw new Exception("Sprite not found");
                }
            }
        }
    }
}