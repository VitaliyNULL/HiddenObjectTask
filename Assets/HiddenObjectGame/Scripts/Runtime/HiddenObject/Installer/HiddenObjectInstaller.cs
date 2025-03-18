using HiddenObjectGame.Runtime.HiddenObject.Interface;
using HiddenObjectGame.Runtime.HiddenObject.Model;
using HiddenObjectGame.Runtime.HiddenObject.ViewModel;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObject.Installer
{

    public class HiddenObjectInstaller : MonoInstaller
    {
        [SerializeField] private HiddenObjectType _hiddenObjectType;

        public override void InstallBindings()
        {
            Container.Bind<IHiddenObjectModel>().FromInstance(new HiddenObjectModel(_hiddenObjectType)).AsSingle();
            Container.Bind<IHiddenObjectViewModel>().To<HiddenObjectViewModel>().AsSingle();
        }
    }
}