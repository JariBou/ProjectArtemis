using System;

public interface IPoolable<T> where T : IPoolable<T>
{
    public void Release();
    public void Init();
    public void Reserve(T copyObject);
    public bool IsAvailable();
}
