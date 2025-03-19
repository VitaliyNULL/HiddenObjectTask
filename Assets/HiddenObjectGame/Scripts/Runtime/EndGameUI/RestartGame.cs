using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HiddenObjectGame.Runtime.EndGameUI
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IDisposable _disposable;

        private void Awake()
        {
            _disposable = _button.OnClickAsObservable().Subscribe((x) =>
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(0);
            });
        }

        
        

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}