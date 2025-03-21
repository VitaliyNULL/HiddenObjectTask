using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObjectGame.Runtime.HiddenObject;
using HiddenObjectGame.Runtime.HiddenObject.View;
using HiddenObjectGame.Runtime.StateMachine;
using HiddenObjectGame.Runtime.StateMachine.States;
using R3;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public class HiddenObjectCollectViewModel : IHiddenObjectCollectViewModel, IDisposable
    {
        private List<HiddenObjectView> _hiddenObjectViews;
        public Dictionary<HiddenObjectType, int> NeedToFoundObjects { get; } = new();
        public ReactiveProperty<bool> Initialized { get; } = new(false);
        public event Action<HiddenObjectType, int> OnFoundedObject;
        private readonly IHiddenObjectCollectModel _collectModel;

        private readonly GameStateMachine _gameStateMachine;
        private readonly SignalBus _signalBus;

        public HiddenObjectCollectViewModel(IHiddenObjectCollectModel collectModel,
            List<HiddenObjectView> hiddenObjectViews, GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            _gameStateMachine = gameStateMachine;
            _collectModel = collectModel;
            _hiddenObjectViews = hiddenObjectViews;
            _signalBus = signalBus;
            _signalBus.Subscribe<ChangeStateSignal>(OnInitialize);
        }

        private void OnInitialize(ChangeStateSignal stateSignal)
        {
            if (stateSignal.CurrentState.GetType() != typeof(InitializeState)) return;
            Debug.Log("Initialize");
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
                _gameStateMachine.ChangeState<EndGameState>();
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
                    _gameStateMachine.ChangeState<EndGameState>();
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
                    Object.Destroy(hiddenObjectView.gameObject);
                }
            }

            _hiddenObjectViews = list;
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ChangeStateSignal>(OnInitialize);
        }
    }
}