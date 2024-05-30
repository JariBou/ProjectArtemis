using Pooling.common;
using UnityEngine;

namespace Pooling.TypedPooling
{
    public abstract class PoolObject<TInfo> : MonoBehaviour, IPoolable<TInfo> where TInfo : PoolMemberInfoBase
    {
        private bool _isActive;

        public void Release()
        {
            _isActive = false;
            Self_Release();
        }

        protected virtual void Self_Release() { }

        public void Init()
        {
            _isActive = true;
            Self_Init();
        }
    
        protected virtual void Self_Init() { }
    
        public void Reserve(TInfo copyObject)
        {
            _isActive = true;
            Self_Reserve(copyObject);
        }
    
        protected virtual void Self_Reserve(TInfo copyObject) { }


        public virtual bool IsAvailable()
        {
            return !_isActive;
        }
    }

    
}