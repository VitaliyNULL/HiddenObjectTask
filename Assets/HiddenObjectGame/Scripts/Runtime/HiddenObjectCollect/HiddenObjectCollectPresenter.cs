using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObjectGame.Runtime.HiddenObject;
using HiddenObjectGame.Runtime.StateMachine;
using HiddenObjectGame.Runtime.StateMachine.States;
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
        private HiddenObjectEntityViewFactory _hiddenObjectEntityViewFactory;
        private IDisposable _disposable;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IHiddenObjectCollectViewModel hiddenObjectCollectViewModel,
            HiddenObjectEntityViewFactory hiddenObjectEntityViewFactory, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _hiddenObjectCollectViewModel = hiddenObjectCollectViewModel;
            _hiddenObjectEntityViewFactory = hiddenObjectEntityViewFactory;
            _disposable = _hiddenObjectCollectViewModel.Initialized.Subscribe(OnInitialized);
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

        private async void OnInitialized(bool obj)
        {
            if (!obj) return;
            foreach (var keyValuePair in _hiddenObjectCollectViewModel.NeedToFoundObjects)
            {
                var spawnedObject =
                    await _hiddenObjectEntityViewFactory.Create(Vector3.zero, Quaternion.identity, _content);
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

            _gameStateMachine.ChangeState<GameState>();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}