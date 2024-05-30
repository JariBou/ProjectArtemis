using Pooling.common;

namespace Pooling.TypedPooling
{
    public interface IPoolable<in TInfo> where TInfo : PoolMemberInfoBase
    {
        public void Release();
        public void Init();
        public void Reserve(TInfo copyObject);
        public bool IsAvailable();
    }
}
