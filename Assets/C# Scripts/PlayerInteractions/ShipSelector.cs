using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ShipSelector : MonoBehaviour
{
    private List<BuildingIdentifyer> selectedBuildingIdentifyerList;

    private bool BuildingsSelected;

    [SerializeField] private Transform selectionAreaTransform;
    private Vector3 startPosition;
    public List<UnitIdentifyer> selectedUnitIdentifyerList;
    private Collider2D[] collider2DArray;

    private TargetingSystem[] targetingSystemArray;

    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyOne;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyTwo;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyThree;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyFour;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyFive;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeySix;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeySeven;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyEight;
    private List<UnitIdentifyer> selectedUnitIdentifyerListKeyNine;

    private bool UnitsSelected;
    private bool HotkeyCycle;

    //private OrderGenerator orderGenerator;

    public UnitController unitController;

    [SerializeField] private LayerMask layerMask;

    private int switchIndex;

    [SerializeField] private GameObject uIButtonManager;

    private UIButtonManager uIButtonManagerScript;

    // Start is called before the first frame update
    private void Awake()
    {
        selectedBuildingIdentifyerList = new List<BuildingIdentifyer>();
        selectedUnitIdentifyerList = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyOne = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyTwo = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyThree = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyFour = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyFive = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeySix = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeySeven = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyEight = new List<UnitIdentifyer>();
        selectedUnitIdentifyerListKeyNine = new List<UnitIdentifyer>();
        selectionAreaTransform.gameObject.SetActive(false);
        //orderGenerator = gameObject.GetComponent<OrderGenerator>();
        uIButtonManagerScript = uIButtonManager.GetComponent<UIButtonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = UtilsClass.GetMouseWorldPosition();
            
        }
        //Gets the upper right and lower left mouse distances from one another and then subtracts them to determine the scale of the "selectionAreaTransform" sprite
        if(Input.GetMouseButton(0)) {
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
            );
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        if(Input.GetMouseButtonUp(0)){
            selectionAreaTransform.gameObject.SetActive(false);

            List<Collider2D> collider2DArray = new List<Collider2D>(Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition(), layerMask));
            for(int i = collider2DArray.Count - 1; i >= 1; i--){
                if(collider2DArray[i].tag != gameObject.tag){
                    collider2DArray.Remove(collider2DArray[i]);
                }
            }

            switchIndex = 0;

            foreach (Collider2D collider2D in collider2DArray) {
                UnitIdentifyer unitIdentifyer = collider2D.GetComponent<UnitIdentifyer>();
                BuildingIdentifyer buildingIdentifyer = collider2D.GetComponent<BuildingIdentifyer>(); 
                if(unitIdentifyer != null) {
                    switchIndex = 1;
                }
                else if(switchIndex != 1 && buildingIdentifyer != null){
                    switchIndex = 2;
                }
            }
            
            //Deselect all Units
            if(Input.GetAxis("LeftShift") <= 0){
                foreach (UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList) {
                    unitIdentifyer.SetHealthVisible(false);
                    unitIdentifyer.SetSelectedVisible(false);
                }
                selectedUnitIdentifyerList.Clear();
            }

            //Deselect all Units
            if(Input.GetAxis("LeftShift") <= 0){
                foreach (BuildingIdentifyer buildingIdentifyer in selectedBuildingIdentifyerList) {
                    buildingIdentifyer.SetHealthVisible(false);
                    buildingIdentifyer.SetSelectedVisible(false);
                    uIButtonManagerScript.ShipFactoryUI(false);
                }
                selectedBuildingIdentifyerList.Clear();
            }

            //Debug.Log(switchIndex);
            switch(switchIndex){
                case 1:
                    foreach (Collider2D collider2D in collider2DArray) {
                        UnitIdentifyer unitIdentifyer = collider2D.GetComponent<UnitIdentifyer>();
                        if(unitIdentifyer != null) {
                            unitIdentifyer.SetSelectedVisible(true);
                            unitIdentifyer.SetHealthVisible(true);
                            selectedUnitIdentifyerList.Add(unitIdentifyer);
                        }
                    }
                    break;

                case 2:
                    foreach (Collider2D collider2D in collider2DArray) {
                        BuildingIdentifyer buildingIdentifyer = collider2D.GetComponent<BuildingIdentifyer>();
                        if(buildingIdentifyer != null) {
                            buildingIdentifyer.SetSelectedVisible(true);
                            buildingIdentifyer.SetHealthVisible(true);
                            selectedBuildingIdentifyerList.Add(buildingIdentifyer);
                            if(collider2D.GetComponent<ShipFactoryBuilding>()){
                                uIButtonManagerScript.ShipFactoryUI(true);
                            }
                        }
                    }
                    break;
            }

            if(collider2DArray.Count == 0){
                UnitsSelected = false;
                HotkeyCycle = false;
            }
            if(collider2DArray.Count > 0){
                UnitsSelected = true;  
            }
            
        //Selects all ships inside the selection area with the UnitIdentifyer script attached
            
            //this.orderGenerator.SetArray(collider2DArray);
        }
            

        if(Input.GetAxis("HotkeyOne") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyOne.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeyOne") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyOne.Clear();
        }

        if(Input.GetAxis("HotkeyOne") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyOne){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }

        if(Input.GetAxis("HotkeyTwo") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyTwo.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeyTwo") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyTwo.Clear();
        }

        if(Input.GetAxis("HotkeyTwo") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyTwo){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }

        if(Input.GetAxis("HotkeyThree") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyThree.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeyThree") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyThree.Clear();
        }
              

        if(Input.GetAxis("HotkeyThree") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyThree){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }
        
        if(Input.GetAxis("HotkeyFour") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyFour.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeyFour") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyFour.Clear();
        }
              
        if(Input.GetAxis("HotkeyFour") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyFour){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }
        
        if(Input.GetAxis("HotkeyFive") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyFive.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeyFive") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyFive.Clear();
        }
              
        if(Input.GetAxis("HotkeyFive") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyFive){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }

        if(Input.GetAxis("HotkeySix") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeySix.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeySix") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeySix.Clear();
        }
              
        if(Input.GetAxis("HotkeySix") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeySix){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }

        if(Input.GetAxis("HotkeySeven") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeySeven.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeySeven") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeySeven.Clear();
        }
              
        if(Input.GetAxis("HotkeySeven") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeySeven){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }

        if(Input.GetAxis("HotkeyEight") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyEight.Add(unitIdentifyer);
            }
        }

        if(Input.GetAxis("HotkeyEight") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyEight.Clear();
        }
              
        if(Input.GetAxis("HotkeyEight") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyEight){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }

        if(Input.GetAxis("HotkeyNine") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerList){
                selectedUnitIdentifyerListKeyNine.Add(unitIdentifyer);
            }
        }
            
        if(Input.GetAxis("HotkeyNine") > 0 && Input.GetAxis("LeftCtrl") > 0 && !HotkeyCycle){
            HotkeyCycle = true;
            selectedUnitIdentifyerListKeyNine.Clear();
        }

        if(Input.GetAxis("HotkeyNine") > 0 && Input.GetAxis("LeftShift") > 0 && !HotkeyCycle && !UnitsSelected){
            HotkeyCycle = true;
            foreach(UnitIdentifyer unitIdentifyer in selectedUnitIdentifyerListKeyNine){
                if(unitIdentifyer != null){
                    unitIdentifyer.SetSelectedVisible(true);
                    unitIdentifyer.SetHealthVisible(true);
                    selectedUnitIdentifyerList.Add(unitIdentifyer);
                }
            }
        }
    }
    public List<UnitIdentifyer> GetUnitList(){
        return selectedUnitIdentifyerList;
    }

    public List<BuildingIdentifyer> GetBuildingList(){
        return selectedBuildingIdentifyerList;
    }
}