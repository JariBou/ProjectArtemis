using System;
using System.Collections.Generic;
using System.Linq;
using Pooling.common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pooling.AnonymousPooling
{
    public class AnonymousPool
    {
        private bool _resizable;
        private int _maxSize;
        private List<AnonymousPoolObject> _poolObjects;
    
        public AnonymousPool()
        {
            _resizable = true;
            _poolObjects = new List<AnonymousPoolObject>();
        }
    
        public AnonymousPool(int size)
        {
            _resizable = false;
            _maxSize = size;
            _poolObjects = new List<AnonymousPoolObject>(size);
        }
    
        public AnonymousPoolObject CreateNewObject(GameObject prefab, PoolMemberInfoBase info, Transform parent = null)
        {
            if (HasAvailablePoolObject(out AnonymousPoolObject freeGameObject))
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
    
        protected AnonymousPoolObject InstantiateNewObject(GameObject prefab, PoolMemberInfoBase info, Transform parent)
        {
            AnonymousPoolObject createdObj = parent == null ? Object.Instantiate(prefab, info.Position, info.Rotation).GetComponent<AnonymousPoolObject>() : Object.Instantiate(prefab, info.Position, info.Rotation, parent).GetComponent<AnonymousPoolObject>();
            createdObj.Init();
            _poolObjects.Add(createdObj);
            //createdObj.TriggerReleaseEvent(ObjectDisabled);
            return createdObj;
        }
    
        public bool HasAvailablePoolObject(out AnonymousPoolObject freeObject)
        {
            freeObject = default;
            foreach (AnonymousPoolObject poolObject in _poolObjects.Where(poolObject => poolObject.IsAvailable()))
            {
                freeObject = poolObject;
                return true;
            }
            return false;
        }

        public void ClearPool(bool callRelease = false)
        {
            foreach (AnonymousPoolObject poolObject in _poolObjects)
            {
                if (callRelease) poolObject.Release();
                _poolObjects.Remove(poolObject);
                Object.Destroy(poolObject.gameObject);
            }
        }

        public void DestroyAllInactive()
        {
            foreach (AnonymousPoolObject poolObject in _poolObjects)
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