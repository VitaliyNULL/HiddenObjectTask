using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.Services
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _hiddenObjectLayer;
        [SerializeField] private HiddenObjectCollectService _hiddenObjectCollectService;
        [SerializeField] private VFXService _vfxService;
        [SerializeField] private HiddenObjectCollectView _hiddenObjectCollectView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HiddenObjectInputService>()
                .FromInstance(new HiddenObjectInputService(_hiddenObjectLayer)).AsSingle();
            Container.Bind<IHiddenObjectCollectService>().FromInstance(_hiddenObjectCollectService).AsSingle();
            Container.Bind<HiddenObjectSaveData>().AsSingle();
            Container.Bind<VFXService>().FromInstance(_vfxService).AsSingle();
            Container.Bind<HiddenObjectCollectView>().FromInstance(_hiddenObjectCollectView).AsSingle();
        }
    }
}