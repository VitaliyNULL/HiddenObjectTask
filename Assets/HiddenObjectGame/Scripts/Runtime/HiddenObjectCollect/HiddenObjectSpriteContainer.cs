using AYellowpaper.SerializedCollections;
using HiddenObjectGame.Runtime.HiddenObject;
using UnityEngine;

namespace HiddenObjectGame.Runtime.HiddenObjectCollect
{
    [CreateAssetMenu(fileName = "HiddenObjectSpriteContainer",
        menuName = "HiddenObjectGame/HiddenObjectSpriteContainer")]
    public class HiddenObjectSpriteContainer : ScriptableObject
    {
        [field: SerializeField]
        public SerializedDictionary<HiddenObjectType, Sprite> SerializedDictionary { get; private set; }

        public bool TryGetSprite(HiddenObjectType type, out Sprite sprite)
        {
            return SerializedDictionary.TryGetValue(type, out sprite);
        }
    }
}