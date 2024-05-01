using _project.Scripts.Pooling;
using UnityEngine;

namespace _project._Tests
{
    public class ProjectileTestScript : PoolObject<TestClass>
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

        protected override void Self_Release()
        {
            gameObject.SetActive(false);
        }

        protected override void Self_Reserve(TestClass copyObject)
        {
            transform.position = copyObject.position;
            
            gameObject.SetActive(true);
        }
    }
}
