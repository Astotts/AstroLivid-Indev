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
        buildingActionPanel.SetActive(false);
        selectedPanel.SetActive(false);
    }

    public void SetBuildingActionActive(bool input){
        return;
        buildingActionPanel.SetActive(input);
        selectedPanel.SetActive(false);
    }

    public void SetSelectedActive(bool input){
        return;
        selectedPanel.SetActive(input);
        buildingActionPanel.SetActive(false);
    }
}
