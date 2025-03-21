using Cysharp.Threading.Tasks;
using HiddenObjectGame.Runtime.HiddenObjectCollect;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace HiddenObjectGame.Runtime.Installers
{
    [CreateAssetMenu(fileName = "FactoryInstaller", menuName = "HiddenObjectGame/Installers/FactoryInstaller")]
    public class FactoryInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetReference _hiddenObjectEntityView;


        public override void InstallBindings()
        {
            Container
                .BindFactory<Vector3, Quaternion, Transform, UniTask<HiddenObjectEntityView>,
                    HiddenObjectEntityViewFactory>().FromIFactory(FactoryBindGenerator);
   
        }

        private void FactoryBindGenerator(
            ConcreteBinderGeneric<IFactory<Vector3, Quaternion, Transform, UniTask<HiddenObjectEntityView>>> obj)
        {
            obj.FromMethod(Method);
        }

        private IFactory<Vector3, Quaternion, Transform, UniTask<HiddenObjectEntityView>> Method(InjectContext arg)
        {
            return new HiddenObjectEntityViewFactory.Factory(arg.Container, _hiddenObjectEntityView);
        }
    }
}