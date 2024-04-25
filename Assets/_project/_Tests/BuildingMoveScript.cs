using System;
using _project.Scripts;
using _project.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _project._Tests
{
    public class BuildingMoveScript : MonoBehaviour
    {

        [SerializeField] private BuildingScript _building;
        private PlayerActions _playerActions;

        [SerializeField] private bool _isMovingBuilding;
        [SerializeField] private LayerMask _responsiveLayers;
        [SerializeField] private LayerMask _groundLayer;
        private int _groundLayerNumber;

        private void Awake()
        {
            _playerActions = new PlayerActions();
            _groundLayerNumber = (int)Math.Log((int)_groundLayer, 2);
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        public void MoveBuilding(GameObject obj)
        {
            if (_isMovingBuilding)
            {
                Destroy(_building.gameObject);
            }

            _building = obj.GetComponent<BuildingScript>();
            _isMovingBuilding = true;
        }

        // Update is called once per frame
        void Update()
        {
        
            if (!_isMovingBuilding) return;
            Vector2 pointerPos = Input.mousePosition;
        
            Ray ray = Camera.main.ScreenPointToRay(pointerPos);

            Debug.DrawRay(ray.origin, ray.direction * 10000);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, float.MaxValue,  _responsiveLayers))
            {
                if (hit.transform.gameObject.layer != _groundLayerNumber)
                {
                    _building.DisallowPlacement();
                }
                else
                {
                    _building.AllowPlacement();
                }
                _building.MoveTo(hit.point);
            }
        
        }
    
        public void DropObject(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
        
            if (PointerUtils.IsPointerOverUI) return;
        
            if (!_building.CanBePlaced) return;
       
            _isMovingBuilding = false;
            _building = null;
        }

  

        public void UpdatePointerPos(InputAction.CallbackContext context)
        {
        }
    
        private void OnEnable()
        {
            _playerActions.BaseActionMap.Enable();
        }

        private void OnDisable()
        {
            _playerActions.BaseActionMap.Disable();
        }
    }
}
