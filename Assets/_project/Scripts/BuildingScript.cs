using System;
using System.Collections.Generic;
using _project.ScriptableObjects.Scripts;
using UnityEngine;

namespace _project.Scripts
{
    public class BuildingScript : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _baseMeshRenderer;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Transform _rangeIndicator;
        [SerializeField] private Transform _shootingStartingPoint;
        private TowerScriptableObject _towerSo;

        private List<Collider> _enteredColliders = new List<Collider>();
        public bool CanBePlaced => _enteredColliders.Count == 0 && _isAllowedToBePlaced;

        public TowerScriptableObject TowerSo => _towerSo;

        public Transform ShootingStartingPoint => _shootingStartingPoint;
        
        private bool _isAllowedToBePlaced = true;
        private bool _isActive;
        
        public bool IsActive => _isActive;

        private void Awake()
        {
            DisableTower();
        }


        public void Config(TowerScriptableObject towerSo)
        {
            _towerSo = towerSo;
            
            _rangeIndicator.localScale = new(TowerSo.BaseRange, 2, TowerSo.BaseRange);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<BuildingScript>()) return;

            DisallowPlacement();
            _enteredColliders.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<BuildingScript>()) return;

            _enteredColliders.Remove(other);
            if (_enteredColliders.Count == 0)
            {
                AllowPlacement();
            }
        }

        private void CheckPlacementState()
        {
            if (CanBePlaced)
            {
                _meshRenderer.material.color = Color.green;
            }
            else
            {
                _meshRenderer.material.color = Color.red;
            }
        }

        public void AllowPlacement()
        {
            _isAllowedToBePlaced = true;
            CheckPlacementState();
        }
    
        public void DisallowPlacement()
        {
            _isAllowedToBePlaced = false;
            CheckPlacementState();
        }

        public void MoveTo(Vector3 pos)
        {
            transform.position = pos;
        }
        
        public void DisableTower()
        {
            SetTowerState(false);
        }

        public void EnableTower()
        {
            SetTowerState(true);
        }

        public void SetTowerState(bool state)
        {
            _isActive = state;
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
}
