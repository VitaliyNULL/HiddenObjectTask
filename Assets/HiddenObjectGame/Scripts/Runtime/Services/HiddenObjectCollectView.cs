using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObjectGame.Runtime.HiddenObject;
using R3;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.Services
{
    public class HiddenObjectCollectView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private HiddenObjectUI _hiddenObjectUIPrefab;
        [SerializeField] private HiddenObjectSpriteContainer _hiddenObjectSpriteContainer;
        private IHiddenObjectCollectService _hiddenObjectCollectService;
        private List<HiddenObjectUI> _hiddenObjectUIs = new List<HiddenObjectUI>();

        [Inject]
        private void Construct(IHiddenObjectCollectService hiddenObjectCollectService)
        {
            _hiddenObjectCollectService = hiddenObjectCollectService;
            hiddenObjectCollectService.Initialized.Subscribe(OnInitialized);
        }
        

        public HiddenObjectUI GetHiddenObjectUI(HiddenObjectType type)
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
            Debug.Log("a");
            foreach (var keyValuePair in _hiddenObjectCollectService.NeedToFoundObjects)
            {
                HiddenObjectUI spawnedObject = Instantiate(_hiddenObjectUIPrefab, _content);
                Sprite sprite = _hiddenObjectSpriteContainer.SerializedDictionary[keyValuePair.Key];
                spawnedObject.SetData(_hiddenObjectCollectService, sprite, keyValuePair.Key, keyValuePair.Value);
                _hiddenObjectUIs.Add(spawnedObject);
            }
        }
    }
}