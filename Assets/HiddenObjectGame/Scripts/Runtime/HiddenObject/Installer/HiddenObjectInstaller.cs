using HiddenObjectGame.Runtime.HiddenObject.Interface;
using HiddenObjectGame.Runtime.HiddenObject.Model;
using HiddenObjectGame.Runtime.HiddenObject.ViewModel;
using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.HiddenObject.Installer
{
    [CreateAssetMenu(fileName = "HiddenObjectInstaller",
        menuName = "HiddenObjectGame/Installers/HiddenObjectInstaller")]
    public class HiddenObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private string _hiddenObjectName;

        public override void InstallBindings()
        {
            Container.Bind<IHiddenObjectModel>().FromInstance(new HiddenObjectModel(_hiddenObjectName)).AsSingle();
            Container.Bind<IHiddenObjectViewModel>().To<HiddenObjectViewModel>().AsSingle();
        }
    }
}