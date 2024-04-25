using _project._Tests;
using _project.ScriptableObjects.Scripts;
using TMPro;
using UnityEngine;

public class BuyTowerButton : MonoBehaviour
{
    [SerializeField] private TowerScriptableObject _towerObject;
    [SerializeField] private BuildingMoveScript _buildingMoveScript;
    [SerializeField] private TMP_Text _towerName;
    
    // Start is called before the first frame update
    void Start()
    {
        _towerName.text = _towerObject.Name;
    }

    public void OnButtonClick()
    {
        _buildingMoveScript.BuyBuilding(_towerObject);
    }
}
