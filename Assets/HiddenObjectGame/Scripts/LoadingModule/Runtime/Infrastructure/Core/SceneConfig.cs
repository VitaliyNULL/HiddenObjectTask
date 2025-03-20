using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BGGames.Runtime.Infrastructure.Core
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "TopDownSurvivor/Configs/SceneConfig")]
    public class SceneConfig : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<SceneType, AssetReference> _scenes;

        public bool TryGetScene(SceneType sceneType, out AssetReference scene)
        {
            return _scenes.TryGetValue(sceneType, out scene);
        }
    }
}