using System;
using System.Collections;
using System.Collections.Generic;
using _project._Tests;
using _project.Scripts;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using UnityEngine;

public class ShootingTestScript : MonoBehaviour
{
    [SerializeField] private BuildingScript _buildingScript;
    private float _timer;
    [ReadOnly] private List<GameObject> _enteredGOs = new();
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyTestScript>())
        {
            _enteredGOs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyTestScript>())
        {
            _enteredGOs.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_enteredGOs.Count == 0) return;
        if (_timer >= _buildingScript.TowerSo.AttackSpeed)
        {
            _timer = 0;
            Instantiate(_buildingScript.TowerSo.ProjectilePrefab, _buildingScript.ShootingStartingPoint.position,
                Quaternion.identity, _buildingScript.ShootingStartingPoint).GetComponent<ProjectileTestScript>().Config(_enteredGOs[0].transform);
        }
    }
}
