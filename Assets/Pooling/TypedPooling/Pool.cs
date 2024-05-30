using System;
using System.Collections.Generic;
using System.Linq;
using Pooling.common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pooling.TypedPooling
{
    public interface IPoolManagementInterface
    {
        public void ClearPool(bool callRelease = false);
        public void DestroyAllInactive();
    }
    
    public class Pool<TObject, TInfo>: IPoolManagementInterface where TObject : MonoBehaviour, IPoolable<TInfo> where TInfo : PoolMemberInfoBase
    {
        private bool _resizable;
        private int _maxSize;
        private List<TObject> _poolObjects;
    
        public Pool()
        {
            _resizable = true;
            _poolObjects = new List<TObject>();
        }
    
        public Pool(int size)
        {
            _resizable = false;
            _maxSize = size;
            _poolObjects = new List<TObject>(size);
        }
    
        public TObject CreateNewObject(GameObject prefab, TInfo info, Transform parent = null)
        {
            if (!prefab.GetComponent<TObject>())
                throw new ArgumentException($"prefab does not have component of type {typeof(TObject)}");
        
            if (HasAvailablePoolObject(out TObject freeGameObject))
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
    
        protected TObject InstantiateNewObject(GameObject prefab, TInfo info, Transform parent)
        {
            TObject createdObj = parent == null ? Object.Instantiate(prefab, info.Position, info.Rotation).GetComponent<TObject>() : Object.Instantiate(prefab, info.Position, info.Rotation, parent).GetComponent<TObject>();
            createdObj.Init();
            _poolObjects.Add(createdObj);
            //createdObj.TriggerReleaseEvent(ObjectDisabled);
            return createdObj;
        }
    
        public bool HasAvailablePoolObject(out TObject freeObject)
        {
            freeObject = default;
            foreach (TObject poolObject in _poolObjects.Where(poolObject => poolObject.IsAvailable()))
            {
                freeObject = poolObject;
                return true;
            }
            return false;
        }

        public void ClearPool(bool callRelease = false)
        {
            foreach (TObject poolObject in _poolObjects)
            {
                if (callRelease) poolObject.Release();
                _poolObjects.Remove(poolObject);
                Object.Destroy(poolObject.gameObject);
            }
        }

        public void DestroyAllInactive()
        {
            foreach (TObject poolObject in _poolObjects)
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