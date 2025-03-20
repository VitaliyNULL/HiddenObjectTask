using HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Core;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.LoadingModule.Runtime.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "BootstrapInstaller", menuName = "Installers/BootstrapInstaller")]
    public class BootstrapInstaller : ScriptableObjectInstaller
    {

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();
        }
    }
}