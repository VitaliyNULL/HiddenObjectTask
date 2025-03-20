using System;
using Cysharp.Threading.Tasks;

namespace BGGames.Runtime.Loading.Interfaces
{
    public interface ILoadingOperation
    {
        public string Description { get; }
        public UniTask Load(Action<float> onProgress);
    }
}