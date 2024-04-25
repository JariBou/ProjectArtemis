using UnityEngine;

namespace _project._Tests
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private BuildingMoveScript _buildingMoveScript;

        [SerializeField] private GameObject _prefab;

        public void OnButtonClick()
        {
            GameObject go = Instantiate(_prefab, Vector3.up*500, Quaternion.identity, transform);
            _buildingMoveScript.MoveBuilding(go);

        }
    }
}
