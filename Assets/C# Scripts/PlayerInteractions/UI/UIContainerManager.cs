using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainerManager : MonoBehaviour
{
    private List<UnitUIContainer> activeContainerList;
    [SerializeField] private List<UnitUIContainer> inactiveContainerList;

    void Start(){
        activeContainerList = new List<UnitUIContainer>();
    }
    
    public void SetUpContainer(UnitVariant variant_){
        return;
        inactiveContainerList[0].SetUpContainer(variant_);
        activeContainerList.Add(inactiveContainerList[0]);
        inactiveContainerList.Remove(inactiveContainerList[0]);
    }

    public void ClearAll(){
        return;
        for(int i = 0; i < activeContainerList.Count; i++){
            activeContainerList[i].ClearContainer();
            inactiveContainerList.Add(activeContainerList[i]);
        }
        activeContainerList.Clear();
    }

    public void ClearAllBuildings(){
        return;
        for(int i = 0; i < activeContainerList.Count; i++){
            if((int)activeContainerList[i].variant < 8){
                Clear(activeContainerList[i]);
                continue;
            }
        }
    }

    private void Clear(UnitUIContainer container){
        return;
        container.ClearContainer();
        inactiveContainerList.Add(container);
        activeContainerList.Remove(container);
    }

    public void AddUnitsToList(List<UnitIdentifyer> clonedList){
        return;
        
        ClearAll();
        
        if(activeContainerList.Count == 0){
            SetUpContainer(clonedList[0].variant);
        }

        foreach(UnitIdentifyer unit in clonedList)
        {
            for(int i = 0; i < activeContainerList.Count; i++){
                if(activeContainerList[i].variant == unit.variant){
                    activeContainerList[i].unitList.Add(unit);
                    break;
                }
                else if(i == activeContainerList.Count - 1){
                    SetUpContainer(unit.variant);
                }
            }
        }

        foreach(UnitUIContainer container in activeContainerList){
            if((int)container.variant >= 8){
                ClearAllBuildings();
                break;   
            }
        }
    
    }
}
