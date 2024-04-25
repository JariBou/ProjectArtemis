using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class TowerScriptableObject : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
    
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
