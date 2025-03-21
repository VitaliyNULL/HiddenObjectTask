using System.Collections.Generic;
using HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Interfaces;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Operations;
using Zenject;

namespace HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Core
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
            ILoadingOperation loadGameplayOperation = new LoadSceneOperation(_sceneLoader, SceneType.Gameplay); ;
            loadingOperations.Enqueue(new LoggerLoadingOperation(loadGameplayOperation));
            loadingOperations.Enqueue(new DelayOperation("Initializing scene...", 1f));
            _loadingController.Load(loadingOperations);
        }
    }
}