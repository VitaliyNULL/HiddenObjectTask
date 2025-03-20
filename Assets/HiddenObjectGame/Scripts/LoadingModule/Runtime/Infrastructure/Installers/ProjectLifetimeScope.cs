using HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Core;
using HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Interfaces;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Core;
using HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "ProjectLifetimeScope", menuName = "Installers/ProjectLifetimeScope")]
    public class ProjectLifetimeScope : ScriptableObjectInstaller
    {
        [SerializeField] private SceneConfig _sceneConfig;

        public override void InstallBindings()
        {
            Container.Bind<ILoadingController>().To<LoadingController>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().WithArguments(_sceneConfig);
        }

       
    }
}