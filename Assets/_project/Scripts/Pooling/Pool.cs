using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool<T> where T : MonoBehaviour, IPoolable<T>
{
    private bool _resizable;
    private int _maxSize;
    private List<T> _poolObjects;
    
    public Pool()
    {
        _resizable = true;
        _poolObjects = new List<T>();
    }
    
    public Pool(int size)
    {
        _resizable = false;
        _maxSize = size;
        _poolObjects = new List<T>(size);
    }

    //TODO: try to fuse these 2 methods
    public T CreateNewObject(T prefab, Transform parent = null)
    {
        if (HasAvailablePoolObject(out T freeGameObject))
        {
            freeGameObject.Reserve(prefab);
            return freeGameObject;
        }

        if (!_resizable && _poolObjects.Count >= _maxSize)
        {  
            return null;
        }
        
        return InstantiateNewObject(prefab, parent);
    }
    
    public T CreateNewObject(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (HasAvailablePoolObject(out T freeGameObject))
        {
            freeGameObject.Reserve(prefab);
            freeGameObject.transform.position = position;
            freeGameObject.transform.rotation = rotation;
            if (parent != null)
            {
                freeGameObject.transform.parent = parent;
            }return freeGameObject;
        }

        if (!_resizable && _poolObjects.Count >= _maxSize)
        {  
            return null;
        }
        
        return InstantiateNewObject(prefab, position, rotation, parent);
    }
    
    //TODO: try to fuse these 2 methods
    public T CreateNewObject(GameObject prefab, Transform parent = null)
    {
        if (!prefab.GetComponent<T>())
            throw new ArgumentException($"prefab does not have component of type {typeof(T)}");
        
        if (HasAvailablePoolObject(out T freeGameObject))
        {
            freeGameObject.Reserve(prefab.GetComponent<T>());
            return freeGameObject;
        }

        if (!_resizable && _poolObjects.Count >= _maxSize)
        {  
            return null;
        }
        
        return InstantiateNewObject(prefab.GetComponent<T>(), parent);
    }
    
    public T CreateNewObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!prefab.GetComponent<T>())
            throw new ArgumentException($"prefab does not have component of type {typeof(T)}");
        
        if (HasAvailablePoolObject(out T freeGameObject))
        {
            freeGameObject.Reserve(prefab.GetComponent<T>());
            freeGameObject.transform.position = position;
            freeGameObject.transform.rotation = rotation;
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
        
        return InstantiateNewObject(prefab.GetComponent<T>(), position, rotation, parent);
    }

    protected T InstantiateNewObject(T prefab, Transform parent)
    {
        T createdObj = parent == null ? Object.Instantiate(prefab).GetComponent<T>() : Object.Instantiate(prefab, parent).GetComponent<T>();
        createdObj.Init();
        _poolObjects.Add(createdObj);
        //createdObj.TriggerReleaseEvent(ObjectDisabled);
        return createdObj;
    }
    
    protected T InstantiateNewObject(T prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        T createdObj = parent == null ? Object.Instantiate(prefab, position, rotation).GetComponent<T>() : Object.Instantiate(prefab, position, rotation, parent).GetComponent<T>();
        createdObj.Init();
        _poolObjects.Add(createdObj);
        //createdObj.TriggerReleaseEvent(ObjectDisabled);
        return createdObj;
    }
    
    public bool HasAvailablePoolObject(out T freeObject)
    {
        freeObject = default;
        foreach (T poolObject in _poolObjects.Where(poolObject => poolObject.IsAvailable()))
        {
            freeObject = poolObject;
            return true;
        }
        return false;
    }
}