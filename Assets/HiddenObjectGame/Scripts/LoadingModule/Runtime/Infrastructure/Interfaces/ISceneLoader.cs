using Cysharp.Threading.Tasks;
using HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Core;

namespace HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Interfaces
{
    public interface ISceneLoader
    {
        public float Progress { get; }

        public UniTask LoadScene(SceneType sceneType);

    }
}