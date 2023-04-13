using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUIPanelController : MonoBehaviour
{
    [SerializeField] GameObject unitActionPanel;
    [SerializeField] GameObject buildingActionPanel;
    [SerializeField] GameObject selectedPanel;

    public void SetUnitActionActive(bool input){
        unitActionPanel.SetActive(input);
    }

    public void SetBuildingActionActive(bool input){
        buildingActionPanel.SetActive(input);
    }

    public void SetSelectedActive(bool input){
        selectedPanel.SetActive(input);
    }
}
