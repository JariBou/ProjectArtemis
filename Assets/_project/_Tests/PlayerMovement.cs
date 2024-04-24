using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _moveVec;
    
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
        transform.position += new Vector3(_moveVec.x, 0, _moveVec.y) * (_speed * Time.fixedDeltaTime); 
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveVec = context.ReadValue<Vector2>();
    }
}
