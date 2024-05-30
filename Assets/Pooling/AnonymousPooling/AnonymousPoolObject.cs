using Pooling.common;
using Pooling.TypedPooling;
using UnityEngine;

namespace Pooling.AnonymousPooling
{
    public abstract class AnonymousPoolObject : MonoBehaviour, IPoolable<PoolMemberInfoBase>
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
    
        public void Reserve(PoolMemberInfoBase copyObject)
        {
            _isActive = true;
            Self_Reserve(copyObject);
        }
    
        protected virtual void Self_Reserve(PoolMemberInfoBase copyObject) { }


        public virtual bool IsAvailable()
        {
            return !_isActive;
        }
    }
}