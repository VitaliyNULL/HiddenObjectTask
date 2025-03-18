using System.Collections.Generic;
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
        private HiddenObjectCollectService _hiddenObjectCollectService;
        private List<HiddenObjectUI> _hiddenObjectUIs = new List<HiddenObjectUI>();

        [Inject]
        private void Construct(HiddenObjectCollectService hiddenObjectCollectService)
        {
            _hiddenObjectCollectService = hiddenObjectCollectService;
            hiddenObjectCollectService.Initialized.Subscribe(OnInitialized);
        }

        public Transform GetHiddenObjectUIPos(HiddenObjectType type)
        {
            return _hiddenObjectUIs.Find(x => x.HiddenObjectType == type).transform;
        }
        private void OnInitialized(bool obj)
        {
            foreach (var keyValuePair in _hiddenObjectCollectService.NeedToFoundObjects)
            {
                HiddenObjectUI spawnedObject = Instantiate(_hiddenObjectUIPrefab, _content);
                Sprite sprite = _hiddenObjectSpriteContainer.SerializedDictionary[keyValuePair.Key];
                spawnedObject.SetData(_hiddenObjectCollectService, sprite, keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}