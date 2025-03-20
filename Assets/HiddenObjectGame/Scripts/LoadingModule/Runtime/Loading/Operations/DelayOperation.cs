using System;
using Cysharp.Threading.Tasks;

namespace BGGames.Runtime.Loading.Operations
{
    public class DelayOperation : BaseLoadingOperation
    {
        private readonly float _seconds;

        public DelayOperation(string description, float seconds)
        {
            Description = description;
            _seconds = seconds;
        }

        public override async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(FullProgress / 2);
            await UniTask.WaitForSeconds(_seconds);
            onProgress?.Invoke(FullProgress);
        }
    }
}