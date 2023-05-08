using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainerManager : MonoBehaviour
{
    private List<UnitUIContainer> activeContainerList;
    [SerializeField] private List<UnitUIContainer> inactiveContainerList;

    List<UnitIdentifyer> clonedList;

    void Start(){
        activeContainerList = new List<UnitUIContainer>();
        clonedList = new List<UnitIdentifyer>();
    }
    
    public void SetUpContainer(UnitType.UnitVariant variant_){
        inactiveContainerList[0].SetUpContainer(variant_);
        activeContainerList.Add(inactiveContainerList[0]);
        inactiveContainerList.Remove(inactiveContainerList[0]);
    }

    public void ClearAll(){
        for(int i = 0; i < activeContainerList.Count; i++){
            activeContainerList[i].ClearContainer();
            inactiveContainerList.Add(activeContainerList[i]);
        }
        activeContainerList.Clear();
    }

    public void AddUnitsToList(List<UnitIdentifyer> unitList){
        clonedList = unitList;

        ClearAll();
        if(activeContainerList.Count == 0){
            SetUpContainer(clonedList[clonedList.Count - 1].variant);
            activeContainerList[0].unitList.Add(clonedList[clonedList.Count - 1]);
        }

        for(int x = clonedList.Count - 1; -1 < x; x--){
            for(int y = activeContainerList.Count - 1; -1 < y; y--){
                if(activeContainerList[y].variant == clonedList[x].variant){
                    activeContainerList[y].unitList.Add(clonedList[x]);
                    //Debug.Log("Added" + clonedList[x].variant);
                    break;
                }
                //Debug.Log("New Container");
                SetUpContainer(clonedList[x].variant);
            }
        }
    }
}
