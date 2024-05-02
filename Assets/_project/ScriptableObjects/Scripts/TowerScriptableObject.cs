using _project._Tests;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using UnityEngine;

namespace _project.ScriptableObjects.Scripts
{
    [Manageable, Editable]
    public class TowerScriptableObject : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private string _name;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed = 1;
        [SerializeField, Range(1, 20)] private float _baseRange;

        public GameObject Prefab => _prefab;
        public string Name => _name;
        public float Health => _health;
        public float Damage => _damage;
        public float BaseRange => _baseRange;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float AttackSpeed => _attackSpeed;
    }
}
