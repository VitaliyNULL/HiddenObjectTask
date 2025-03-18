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
        private HiddenObjectCollectService _hiddenObjectCollectService;

        public void SetData(HiddenObjectCollectService hiddenObjectCollectService, Sprite sprite, HiddenObjectType type,
            int count)
        {
            HiddenObjectType = type;
            _hiddenObjectCollectService = hiddenObjectCollectService;
            _image.sprite = sprite;
            _text.text = count.ToString();
            _hiddenObjectCollectService.OnFoundedObject += OnFoundedObject;
        }

        private void OnFoundedObject()
        {
            if (_hiddenObjectCollectService.NeedToFoundObjects.TryGetValue(HiddenObjectType, out int count))
            {
                _text.text = count.ToString();
            }
        }
    }
}