using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ShipSelector : MonoBehaviour {

    [SerializeField] private Transform selectionAreaTransform;
    [SerializeField] private ActiveUIPanelController activeUIController;
    [SerializeField] private UIContainerManager containerManager;

    private Vector3 startPosition;
    private List<UnitIdentifyer> selectedUnitIdentifyerList;

    private void Awake() {
        selectedUnitIdentifyerList = new List<UnitIdentifyer>();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Left Mouse Button Pressed
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = UtilsClass.GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0)) {
            // Left Mouse Button Held Down
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

        if (Input.GetMouseButtonUp(0)) {
            // Left Mouse Button Released
            selectionAreaTransform.gameObject.SetActive(false);

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

            // Deselect all Units
            foreach (UnitIdentifyer unit in selectedUnitIdentifyerList) {
                unit.SetSelectedVisible(false);
                unit.selected = false;
            }
            selectedUnitIdentifyerList.Clear();

            // Select Units within Selection Area
            foreach (Collider2D collider2D in collider2DArray) {
                UnitIdentifyer unit = collider2D.GetComponent<UnitIdentifyer>();
                if (unit != null) {
                    unit.SetSelectedVisible(true);
                    unit.selected = true;
                    selectedUnitIdentifyerList.Add(unit);
                }
            }

            if(selectedUnitIdentifyerList.Count > 0){
                containerManager.AddUnitsToList(selectedUnitIdentifyerList);
                activeUIController.SetSelectedActive(true);
            }
            else{
                activeUIController.SetSelectedActive(false);
                containerManager.ClearAll();
            }

            //Debug.Log(selectedUnitIdentifyerList.Count);
        }

        if (Input.GetMouseButtonDown(1)) {
            // Right Mouse Button Pressed
            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition();

            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 10f, 20f, 30f }, new int[] { 5, 10, 20 });

            int targetPositionListIndex = 0;

            foreach (UnitIdentifyer unit in selectedUnitIdentifyerList) {
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
