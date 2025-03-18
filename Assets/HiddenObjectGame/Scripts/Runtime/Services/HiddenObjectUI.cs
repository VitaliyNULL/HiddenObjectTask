using HiddenObjectGame.Runtime.HiddenObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HiddenObjectGame.Runtime.Services
{
    public class HiddenObjectUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        public HiddenObjectType HiddenObjectType { get; private set; }
        private IHiddenObjectCollectService _hiddenObjectCollectService;

        private int _count;
        public void SetData(IHiddenObjectCollectService hiddenObjectCollectService, Sprite sprite,
            HiddenObjectType type,
            int count)
        {
            HiddenObjectType = type;
            _hiddenObjectCollectService = hiddenObjectCollectService;
            _image.sprite = sprite;
            _text.text = count.ToString();
            _hiddenObjectCollectService.OnFoundedObject += OnFoundedObject;
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
    }
}