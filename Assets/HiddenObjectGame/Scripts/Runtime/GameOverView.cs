using System;
using Cysharp.Threading.Tasks;
using HiddenObjectGame.Runtime.Services;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HiddenObjectGame.Runtime
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _popUp;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private GameObject _bottomPanel;
        private IDisposable _disposable;
        private Color _initialColor;

        [Inject]
        private void Construct(IHiddenObjectCollectService hiddenObjectCollectService)
        {
            _disposable = hiddenObjectCollectService.OnAllFounded.Subscribe((allFounded =>
            {
                if (allFounded) ShowPopUp().Forget();
            }));
            _initialColor = _image.color;
        }

        private async UniTaskVoid ShowPopUp()
        {
            await UniTask.WaitForSeconds(1f);
            _bottomPanel.SetActive(false);
            _image.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 0);
            Color startColor = _image.color;
            _popUp.localScale = Vector3.zero;
            float elapsedTime = 0;
            gameObject.SetActive(true);
            while (elapsedTime < _duration)
            {
                _image.color = Color.Lerp(startColor,
                    _initialColor, elapsedTime / _duration);
                elapsedTime += Time.deltaTime;
                _popUp.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / _duration);
                await UniTask.WaitForEndOfFrame();
            }

            _popUp.localScale = Vector3.one;
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}