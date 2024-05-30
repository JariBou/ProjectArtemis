using System;
using System.Collections.Generic;
using Pooling.common;
using UnityEngine;

namespace Pooling.TypedPooling
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager Instance;

        private Dictionary<Type, IPoolManagementInterface> _dictionary = new ();

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

        public static Pool<TObject, TInfo> RequestPoolOfType<TObject, TInfo>() 
            where TObject : MonoBehaviour, IPoolable<TInfo>
            where TInfo : PoolMemberInfoBase
        {
            if (Instance._dictionary.TryGetValue(typeof(TObject), out IPoolManagementInterface poolManagementInterface))
            {
                return (Pool<TObject, TInfo>)poolManagementInterface;
            }

            Pool<TObject, TInfo> newPool = new Pool<TObject, TInfo>();
            Instance._dictionary.Add(typeof(TObject), newPool);
            return newPool;
        }

        // ========= Justin Case ================================
        public static void DestroyAllInactiveOfPoolOfType<TObject, TInfo>()
            where TObject : MonoBehaviour, IPoolable<TInfo>
            where TInfo : PoolMemberInfoBase
        {
            if (Instance._dictionary.TryGetValue(typeof(TObject), out IPoolManagementInterface poolManagementInterface))
            {
                poolManagementInterface.DestroyAllInactive();
            }
        }
        
        public static void DestroyAllInactiveOfAllPools()
        {
            foreach (IPoolManagementInterface poolManagementInterface in Instance._dictionary.Values)
            {
                poolManagementInterface.DestroyAllInactive();
            }
        }
        
        public static void ClearPoolOfType<TObject, TInfo>()
            where TObject : MonoBehaviour, IPoolable<TInfo>
            where TInfo : PoolMemberInfoBase
        {
            if (Instance._dictionary.TryGetValue(typeof(TObject), out IPoolManagementInterface poolManagementInterface))
            {
                poolManagementInterface.ClearPool();
            }
        }

        public static void ClearAllPools()
        {
            foreach (IPoolManagementInterface poolManagementInterface in Instance._dictionary.Values)
            {
                poolManagementInterface.ClearPool();
            }
        }
        // ======================================================
    }
}