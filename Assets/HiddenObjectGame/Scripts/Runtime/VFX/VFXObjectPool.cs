namespace HiddenObjectGame.Runtime.VFX
{
    public class VFXObjectPool : ObjectPool<VFXInstance>
    {
        public VFXObjectPool(VFXInstance prefab, int initialSize = 5, int maxSize = 10, int growthRate = 1) : base(
            prefab, initialSize, maxSize, growthRate)
        {
        }

        protected override void InitializePoolObject(VFXInstance instance)
        {
            instance.Initialize(this);
        }
    }
}