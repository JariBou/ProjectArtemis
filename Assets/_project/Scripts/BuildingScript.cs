using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts
{
    public class BuildingScript : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private List<Collider> _enteredColliders = new List<Collider>();
        public bool CanBePlaced => _enteredColliders.Count == 0 && _isAllowedToBePlaced;
        private bool _isAllowedToBePlaced = true;


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
