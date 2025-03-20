using BGGames.Runtime.Infrastructure.Core;
using BGGames.Runtime.Infrastructure.Interfaces;
using BGGames.Runtime.Loading.Core;
using BGGames.Runtime.Loading.Interfaces;
using UnityEngine;
using Zenject;

namespace BGGames.Runtime.Infrastructure.Installers
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