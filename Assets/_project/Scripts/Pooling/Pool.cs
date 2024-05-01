using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _project.Scripts.Pooling
{
    public class Pool<TObject, TInfo> where TObject : MonoBehaviour, IPoolable<TInfo> where TInfo : PoolMemberInfoBase
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
    
        public TObject CreateNewObject(TObject prefab, TInfo info, Transform parent = null)
        {
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
        
            return InstantiateNewObject(prefab.GetComponent<TObject>(), info, parent);
        }
    
        protected TObject InstantiateNewObject(TObject prefab, TInfo info, Transform parent)
        {
            TObject createdObj = parent == null ? Object.Instantiate(prefab, info.position, info.rotation).GetComponent<TObject>() : Object.Instantiate(prefab, info.position, info.rotation, parent).GetComponent<TObject>();
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
    }
}