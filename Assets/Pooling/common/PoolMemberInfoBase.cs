using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Pooling.common
{
    public class PoolMemberInfoBase
    {
        public Vector3 Position {get; set;}
        public Quaternion Rotation {get; set;}
        public Transform Parent {get; set;}
    
        public PoolMemberInfoBase() { }
    
        public PoolMemberInfoBase(Vector3 position, Quaternion rotation)
        {
            position = position;
            rotation = rotation;
            Parent = null;
        }
    
        public PoolMemberInfoBase(Vector3 position, Quaternion rotation, Transform parent)
        {
            position = position;
            rotation = rotation;
            parent = parent;
        }
    }
}