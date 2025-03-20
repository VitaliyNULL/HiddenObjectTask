using System.Collections.Generic;
using BGGames.Runtime.Infrastructure.Interfaces;
using BGGames.Runtime.Loading.Interfaces;
using BGGames.Runtime.Loading.Operations;
using Zenject;

namespace BGGames.Runtime.Infrastructure.Core
{
    public class Bootstrap : IInitializable
    {
        private readonly ILoadingController _loadingController;
        private readonly ISceneLoader _sceneLoader;

        public Bootstrap(ILoadingController loadingController, ISceneLoader sceneLoader)
        {
            _loadingController = loadingController;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            Queue<ILoadingOperation> loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new LoadSceneOperation(_sceneLoader, SceneType.Gameplay));
            loadingOperations.Enqueue(new DelayOperation("Initializing scene...", 1f));
            _loadingController.Load(loadingOperations);
        }
    }
}