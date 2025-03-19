using HiddenObjectGame.Runtime.HiddenObjectCollect;
using HiddenObjectGame.Runtime.InputService;
using HiddenObjectGame.Runtime.VFX;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _hiddenObjectLayer;
        [SerializeField] private HiddenObjectCollectViewModel _hiddenObjectCollectViewModel;
        [SerializeField] private VFXService _vfxService;
        [SerializeField] private HiddenObjectCollectView _hiddenObjectCollectView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HiddenObjectInputService>()
                .FromInstance(new HiddenObjectInputService(_hiddenObjectLayer)).AsSingle();
            Container.Bind<IHiddenObjectCollectViewModel>().FromInstance(_hiddenObjectCollectViewModel).AsSingle();
            Container.Bind<IHiddenObjectCollectModel>().To<HiddenObjectCollectModel>().AsSingle();
            Container.Bind<VFXService>().FromInstance(_vfxService).AsSingle();
            Container.Bind<HiddenObjectCollectView>().FromInstance(_hiddenObjectCollectView).AsSingle();
        }
    }
}