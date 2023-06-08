using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelector : MonoBehaviour {

    [SerializeField] private Transform selectionAreaTransform;
    [SerializeField] private ActiveUIPanelController activeUIController;
    [SerializeField] private UIContainerManager containerManager;

    private Vector3 startPosition;

    private List<UnitIdentifyer> selectedUnitList;

    private List<UnitIdentifyer> unitIdentifyerList;
    private HashSet<UnitVariant> selectedUnitSet;

    private bool buildUI;

    private void Awake() {
        selectedUnitSet = new HashSet<UnitVariant>();
        selectedUnitList = new List<UnitIdentifyer>();
        unitIdentifyerList = new List<UnitIdentifyer>();
        selectionAreaTransform.gameObject.SetActive(false);
        selectedUnitSet.Clear();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Left Mouse Button Pressed
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {
            // Left Mouse Button Held Down
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        if (Input.GetMouseButtonUp(0)) {
            // Left Mouse Button Released
            // Deselect all Units
            foreach (UnitIdentifyer unit in unitIdentifyerList) {
                unit.SetSelectedVisible(false);
                unit.selected = false;
            }
            unitIdentifyerList.Clear();

            foreach (UnitIdentifyer unit in selectedUnitList) {
                unit.SetSelectedVisible(false);
                unit.selected = false;
            }
            selectedUnitList.Clear();
            
            selectionAreaTransform.gameObject.SetActive(false);

            // Select Units within Selection Area

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            foreach (Collider2D collider2D in collider2DArray) {
                UnitIdentifyer unit = collider2D.GetComponent<UnitIdentifyer>();
                if (unit != null) {
                    if(!selectedUnitSet.Contains(unit.variant)){
                        selectedUnitSet.Add(unit.variant);
                    }
                    unit.SetSelectedVisible(true);
                    unit.selected = true;
                    unitIdentifyerList.Add(unit);
                }
            }

            for(ushort x = 0; x < 8; x++){
                if(selectedUnitSet.Contains((UnitVariant)x) && unitIdentifyerList.Count > 0){
                    for(int y = unitIdentifyerList.Count - 1; y >= 0; y--){
                        if((int)unitIdentifyerList[y].variant < 8){
                            //Debug.Log("Removing " + y);
                            //Debug.Log("Count " + unitIdentifyerList.Count);
                            unitIdentifyerList[y].SetSelectedVisible(false);
                            unitIdentifyerList[y].selected = false;
                            unitIdentifyerList.Remove(unitIdentifyerList[y]);
                        }
                    }        
                    break;
                }
            }
            selectedUnitList = unitIdentifyerList;

            if(selectedUnitList.Count > 0){
                foreach(UnitIdentifyer unit in selectedUnitList){
                    if((ushort)unit.variant > 7){
                        containerManager.AddUnitsToList(selectedUnitList);
                        activeUIController.SetSelectedActive(true);
                        buildUI = false;
                        break;
                    }
                    buildUI = true;
                }

                if(buildUI){
                    activeUIController.SetBuildingActionActive(true);
                }

            }
            else{
                activeUIController.SetSelectedActive(false);
                containerManager.ClearAll();
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            // Right Mouse Button Pressed
            Vector3 moveToPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 10f, 20f, 30f }, new int[] { 5, 10, 20 });

            int targetPositionListIndex = 0;

            foreach (UnitIdentifyer unit in selectedUnitList) {
                Debug.Log(unit);
                unit.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex++;
            }
        }
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray) {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++) {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount) {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++) {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle) {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

}
