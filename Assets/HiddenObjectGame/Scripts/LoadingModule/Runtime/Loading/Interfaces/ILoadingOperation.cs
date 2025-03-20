using System;
using Cysharp.Threading.Tasks;

namespace HiddenObjectGame.LoadingModule.Runtime.Loading.Interfaces
{
    public interface ILoadingOperation
    {
        public string Description { get; }
        public UniTask Load(Action<float> onProgress);
    }
}