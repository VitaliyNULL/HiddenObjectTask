using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.Runtime.HiddenObjectCollect;
using HiddenObjectGame.Runtime.StateMachine.States;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HiddenObjectGame.Runtime.EndGameUI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _popUp;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private GameObject _bottomPanel;
        private Color _initialColor;
        private CancellationTokenSource _cancellationTokenSource;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _initialColor = _image.color;
            _signalBus.Subscribe<ChangeStateSignal>(Callback);
        }

        private void Awake()
        {
            Debug.Log("Awake");
            gameObject.SetActive(false);
        }

        private void Callback(ChangeStateSignal obj)
        {
            if (obj.CurrentState.GetType() == typeof(EndGameState))
                ShowPopUp().Forget();
        }

        private async UniTaskVoid ShowPopUp()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await UniTask.WaitForSeconds(1f).AttachExternalCancellation(_cancellationTokenSource.Token);
            if (_cancellationTokenSource.Token.IsCancellationRequested) return;
            _bottomPanel.SetActive(false);
            _image.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 0);
            Color startColor = _image.color;
            _popUp.localScale = Vector3.zero;
            float elapsedTime = 0;
            gameObject.SetActive(true);
            while (elapsedTime < _duration)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested) break;
                _image.color = Color.Lerp(startColor,
                    _initialColor, elapsedTime / _duration);
                elapsedTime += Time.deltaTime;
                _popUp.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / _duration);
                await UniTask.WaitForEndOfFrame(_cancellationTokenSource.Token);
            }

            _popUp.localScale = Vector3.one;
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<ChangeStateSignal>(Callback);
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}