using System;
using UnityEngine;

namespace _project._Tests
{
    public class ProjectileTestScript : MonoBehaviour
    {
        [SerializeField] private float _speed = 10;
        private Transform _target;

        public void Config(Transform target)
        {
            _target = target;
        }

        private void FixedUpdate()
        {
            if (_target == null) return;
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * (_speed * Time.fixedDeltaTime);
        }
    }
}