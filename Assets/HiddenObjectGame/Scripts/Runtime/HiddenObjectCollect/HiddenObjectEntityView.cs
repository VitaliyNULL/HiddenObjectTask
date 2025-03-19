using HiddenObjectGame.Runtime.HiddenObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    public class HiddenObjectEntityView : MonoBehaviour, IHiddenObjectEntityView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        public HiddenObjectType HiddenObjectType { get; private set; }
        private IHiddenObjectCollectViewModel _hiddenObjectCollectViewModel;

        private int _count;
        public void SetData(IHiddenObjectCollectViewModel hiddenObjectCollectViewModel, Sprite sprite,
            HiddenObjectType type,
            int count)
        {
            HiddenObjectType = type;
            _hiddenObjectCollectViewModel = hiddenObjectCollectViewModel;
            _image.sprite = sprite;
            _text.text = count.ToString();
            _hiddenObjectCollectViewModel.OnFoundedObject += OnFoundedObject;
        }

        private void OnFoundedObject(HiddenObjectType type, int count)
        {
            if (type != HiddenObjectType) return;
            _count = count;
        }

        public void CompleteObjectCollection()
        {
            if (_count <= 0)
            {
                Destroy(gameObject);
                return;
            }
            _text.text = _count.ToString();
        }

        public Transform GetTransform() => transform;
    }
}