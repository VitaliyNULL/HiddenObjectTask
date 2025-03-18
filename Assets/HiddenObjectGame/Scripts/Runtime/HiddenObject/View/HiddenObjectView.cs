using HiddenObjectGame.Runtime.HiddenObject.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObject.View
{
    public class HiddenObjectView : MonoBehaviour, IHiddenObjectView
    {
        private IHiddenObjectViewModel _viewModel;

        [Inject]
        private void Construct(IHiddenObjectViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnClick()
        {
            _viewModel.OnClicked();
            Debug.Log("Clicked");
        }
    }
}