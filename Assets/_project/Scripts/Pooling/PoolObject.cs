using UnityEngine;

namespace _project.Scripts.Pooling
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

    public class PoolMemberInfoBase
    {
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent = null;
    
        public PoolMemberInfoBase() { }
    
        public PoolMemberInfoBase(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    
        public PoolMemberInfoBase(Vector3 position, Quaternion rotation, Transform parent)
        {
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
        }
    }
}