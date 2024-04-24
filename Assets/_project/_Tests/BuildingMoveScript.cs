using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BuildingMoveScript : MonoBehaviour
{

    [SerializeField] private GameObject _building;
    private float _yOffset;
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
        _building = obj;
        _yOffset = _building.GetComponent<MeshRenderer>().bounds.extents.y;
        _isMovingBuilding = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropObject(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _isMovingBuilding = false;
        _building = null;
    }

  

    public void UpdatePointerPos(InputAction.CallbackContext context)
    {
        if (!_isMovingBuilding) return;

        Vector2 pointerPos = context.ReadValue<Vector2>();
        
        Ray ray = Camera.main.ScreenPointToRay(pointerPos);

        Debug.DrawRay(ray.origin, ray.direction * 10000);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, float.MaxValue,  _responsiveLayers))
        {
            if (hit.transform.gameObject.layer != _groundLayerNumber) return;
            _building.transform.position = new Vector3(hit.point.x, hit.point.y + _yOffset, hit.point.z);
        }

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
