using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScriptTest : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private List<Collider> _enteredColliders = new List<Collider>();


    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<BuildingScriptTest>()) return;
        
        _meshRenderer.material.color = Color.red;
        _enteredColliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<BuildingScriptTest>()) return;

        _enteredColliders.Remove(other);
        if (_enteredColliders.Count == 0)
        {
            _meshRenderer.material.color = Color.green;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
