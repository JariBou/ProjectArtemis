using System;
using System.Collections.Generic;
using System.Linq;
using Pooling.common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pooling.TypedPooling
{
    
    public class SimplePool<TInfo>: IPoolManagementInterface where TInfo : PoolMemberInfoBase
    {
        private bool _resizable;
        private int _maxSize;
        private List<SimplePoolObject<TInfo>> _poolObjects;
    
        public SimplePool()
        {
            _resizable = true;
            _poolObjects = new List<SimplePoolObject<TInfo>>();
        }
    
        public SimplePool(int size)
        {
            _resizable = false;
            _maxSize = size;
            _poolObjects = new List<SimplePoolObject<TInfo>>(size);
        }
    
        public SimplePoolObject<TInfo> CreateNewObject(GameObject prefab, TInfo info, Transform parent = null)
        {
            if (!prefab.GetComponent<SimplePoolObject<TInfo>>())
                throw new ArgumentException($"prefab does not have component of type {typeof(SimplePoolObject<TInfo>)}");
        
            if (HasAvailablePoolObject(out SimplePoolObject<TInfo> freeGameObject))
            {
                freeGameObject.Reserve(info);
                if (parent != null)
                {
                    freeGameObject.transform.parent = parent;
                }
                return freeGameObject;
            }

            if (!_resizable && _poolObjects.Count >= _maxSize)
            {  
                return null;
            }
        
            return InstantiateNewObject(prefab, info, parent);
        }
    
        protected SimplePoolObject<TInfo> InstantiateNewObject(GameObject prefab, TInfo info, Transform parent)
        {
            SimplePoolObject<TInfo> createdObj = parent == null ? Object.Instantiate(prefab, info.Position, info.Rotation).GetComponent<SimplePoolObject<TInfo>>() : Object.Instantiate(prefab, info.Position, info.Rotation, parent).GetComponent<SimplePoolObject<TInfo>>();
            createdObj.Init();
            _poolObjects.Add(createdObj);
            //createdObj.TriggerReleaseEvent(ObjectDisabled);
            return createdObj;
        }
    
        public bool HasAvailablePoolObject(out SimplePoolObject<TInfo> freeObject)
        {
            freeObject = default;
            foreach (SimplePoolObject<TInfo> poolObject in _poolObjects.Where(poolObject => poolObject.IsAvailable()))
            {
                freeObject = poolObject;
                return true;
            }
            return false;
        }

        public void ClearPool(bool callRelease = false)
        {
            foreach (SimplePoolObject<TInfo> poolObject in _poolObjects)
            {
                if (callRelease) poolObject.Release();
                _poolObjects.Remove(poolObject);
                Object.Destroy(poolObject.gameObject);
            }
        }

        public void DestroyAllInactive()
        {
            foreach (SimplePoolObject<TInfo> poolObject in _poolObjects)
            {
                if (poolObject.IsAvailable())
                {
                    _poolObjects.Remove(poolObject);
                    Object.Destroy(poolObject.gameObject);
                }
            }
        }
    }
}