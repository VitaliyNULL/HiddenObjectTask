using BGGames.Runtime.Infrastructure.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace BGGames.Runtime.Infrastructure.Installers
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