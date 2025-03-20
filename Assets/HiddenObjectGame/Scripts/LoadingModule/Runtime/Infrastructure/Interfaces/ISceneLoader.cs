using BGGames.Runtime.Infrastructure.Core;
using Cysharp.Threading.Tasks;

namespace BGGames.Runtime.Infrastructure.Interfaces
{
    public interface ISceneLoader
    {
        public float Progress { get; }

        public UniTask LoadScene(SceneType sceneType);

    }
}