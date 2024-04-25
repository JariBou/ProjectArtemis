using UnityEngine;
using UnityEngine.InputSystem;

namespace _project._Tests
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Vector2 _moveVec;

        [SerializeField] private Transform _cameraTransform; 
        private Vector3 _forwardVec;
        private Vector3 _rightVec;

        private void Awake()
        {
            _forwardVec = _cameraTransform.forward;
            _forwardVec.y = 0;
            _rightVec = _cameraTransform.right;
            _rightVec.y = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            transform.position += (_forwardVec * _moveVec.y + _rightVec * _moveVec.x) * (_speed * Time.fixedDeltaTime); 
        }

        public void Move(InputAction.CallbackContext context)
        {
            _moveVec = context.ReadValue<Vector2>();
        }
    }
}
