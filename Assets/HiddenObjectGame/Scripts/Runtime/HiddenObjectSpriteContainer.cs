using AYellowpaper.SerializedCollections;
using HiddenObjectGame.Runtime.HiddenObject;
using UnityEngine;

namespace HiddenObjectGame.Runtime
{
    [CreateAssetMenu(fileName = "HiddenObjectSpriteContainer",
        menuName = "HiddenObjectGame/HiddenObjectSpriteContainer")]
    public class HiddenObjectSpriteContainer : ScriptableObject
    {
        [field: SerializeField]
        public SerializedDictionary<HiddenObjectType, Sprite> SerializedDictionary { get; private set; }
    }
}