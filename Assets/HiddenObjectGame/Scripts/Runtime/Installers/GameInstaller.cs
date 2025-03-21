using System.Collections.Generic;
using HiddenObjectGame.Runtime.HiddenObject.View;
using HiddenObjectGame.Runtime.HiddenObjectCollect;
using HiddenObjectGame.Runtime.InputService;
using HiddenObjectGame.Runtime.StateMachine;
using HiddenObjectGame.Runtime.StateMachine.States;
using HiddenObjectGame.Runtime.VFX;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _hiddenObjectLayer;
        [SerializeField] private VFXService _vfxService;
        [SerializeField] private HiddenObjectCollectPresenter _hiddenObjectCollectPresenter;
        [SerializeField] private List<HiddenObjectView> _hiddenObjectViews;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HiddenObjectInputService>()
                .FromInstance(new HiddenObjectInputService(_hiddenObjectLayer)).AsSingle();
            Container.Bind<IHiddenObjectCollectModel>().To<HiddenObjectCollectModel>().AsSingle();
            Container.Bind<VFXService>().FromInstance(_vfxService).AsSingle();
            Container.Bind<HiddenObjectCollectPresenter>().FromInstance(_hiddenObjectCollectPresenter).AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadCollectModelService>().AsSingle();
            Container.BindInterfacesAndSelfTo<HiddenObjectCollectViewModel>().AsSingle()
                .WithArguments(_hiddenObjectViews);
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ChangeStateSignal>().OptionalSubscriber();
        }
    }
}