using System;
using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Pooling
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager Instance;

        private Dictionary<Type, Pool<PoolObject<PoolMemberInfoBase>, PoolMemberInfoBase>> _dictionary = new ();


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public static Pool<TObject, TInfo> RequestPool<TObject, TInfo>() 
            where TObject : PoolObject<TInfo>
            where TInfo : PoolMemberInfoBase
        {
            foreach (KeyValuePair<Type,Pool<PoolObject<PoolMemberInfoBase>,PoolMemberInfoBase>> keyValuePair in Instance._dictionary)
            {
                Debug.Log(keyValuePair.Value);
            }
            
            if (Instance._dictionary.TryGetValue(typeof(TObject), out Pool<PoolObject<PoolMemberInfoBase>, PoolMemberInfoBase> value))
            {
                Debug.Log("Surpise");
                return (Pool<TObject, TInfo>)value;
            }
            Pool<TObject, TInfo> pool = new Pool<TObject, TInfo>();
            // Instance._dictionary.Add(typeof(TObject), pool as Pool<PoolObject<PoolMemberInfoBase>, PoolMemberInfoBase>);
            Instance._dictionary.Add(typeof(TObject), (Pool<PoolObject<PoolMemberInfoBase>, PoolMemberInfoBase>) pool);
            
            return pool;
        }

    }
}