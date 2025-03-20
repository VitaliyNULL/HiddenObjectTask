using BGGames.Runtime.Loading.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace BGGames.Runtime.Loading.View
{
    public abstract class BaseLoadingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;
        private ILoadingController _loadingController;
        protected float Progress;
        
        [Inject]
        public void Construct(ILoadingController loadingController)
        {
            _loadingController = loadingController;
            _loadingController.OnStartLoading += ShowLoading;
            _loadingController.OnFinishLoading += HideLoading;
            _loadingController.OnProgress += SetProgress;
        }

        protected virtual void ShowLoading()
        {
            Progress = 0;
            gameObject.SetActive(true);
        }

        protected virtual void HideLoading()
        {
            _descriptionText.text = string.Empty;
            gameObject.SetActive(false);
        }

        private void SetProgress(string description, float progress)
        {
            _descriptionText.text = description;
            Progress = progress;
        }

        private void OnDestroy()
        {
            _loadingController.OnStartLoading -= ShowLoading;
            _loadingController.OnFinishLoading -= HideLoading;
            _loadingController.OnProgress -= SetProgress;
        }
    }
}