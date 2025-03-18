using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.Services
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _hiddenObjectLayer;
        [SerializeField] private HiddenObjectCollectService _hiddenObjectCollectService;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HiddenObjectInputService>()
                .FromInstance(new HiddenObjectInputService(_hiddenObjectLayer)).AsSingle();
            Container.Bind<IHiddenObjectCollectService>().FromInstance(_hiddenObjectCollectService).AsSingle();
            Container.Bind<HiddenObjectSaveData>().AsSingle();
        }
    }
}