using UnityEngine;
using Zenject;

namespace HiddenObjectGame.Runtime.Services
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "HiddenObjectGame/Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LayerMask _hiddenObjectLayer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HiddenObjectInputService>()
                .FromInstance(new HiddenObjectInputService(_hiddenObjectLayer)).AsSingle();
        }
    }
}