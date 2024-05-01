using System;
using UnityEngine;

public abstract class PoolObject<T> : MonoBehaviour, IPoolable<T> where T : PoolObject<T>
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
    
    public void Reserve(T copyObject)
    {
        _isActive = true;
        Self_Reserve(copyObject);
    }
    
    protected virtual void Self_Reserve(T copyObject) { }


    public virtual bool IsAvailable()
    {
        return !_isActive;
    }
}