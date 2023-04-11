/*using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public enum FlowFieldDisplayType { None, AllIcons, DestinationIcon, CostField, IntegrationField };

public class GridDebug : MonoBehaviour
{
	public GridController gridController;
	public bool displayGrid;

	public FlowFieldDisplayType curDisplayType;

	private Vector2Int gridSize;
	private float cellRadius;
	private FlowField curFlowField;

	private Sprite[] ffIcons;

	private void Start()
	{
		ffIcons = Resources.LoadAll<Sprite>("Sprites/FFicons");
	}

	public void SetFlowField(FlowField newFlowField)
	{
		curFlowField = newFlowField;
		cellRadius = newFlowField.cellRadius;
		gridSize = newFlowField.gridSize;
	}
	
	public void DrawFlowField(List<Vector2Int> gridList, Cell[,] grid)
	{
		ClearCellDisplay();

		switch (curDisplayType)
		{
			case FlowFieldDisplayType.AllIcons:
				DisplayAllCells(gridList, grid);
				break;

			case FlowFieldDisplayType.DestinationIcon:
				DisplayDestinationCell();
				break;

			default:
				break;
		}
	}

	private void DisplayAllCells(List<Vector2Int> gridList, Cell[,] grid)
	{
		if (curFlowField == null) { return; }
		for(int i = 0; gridList.Count - 1 >= i; i++){
			for(int x = (gridList[i].x * 10); x < (gridList[i].x * 10) + 10; x++){
				for(int y = (gridList[i].y * 10); y < (gridList[i].y * 10) + 10; y++){
					DisplayCell(grid[x,y]);
				}
			}
		}
	}

	private void DisplayDestinationCell()
	{
		if (curFlowField == null) { return; }
		DisplayCell(curFlowField.destinationCell);
	}

	private void DisplayCell(Cell cell)
	{
		/* Cells are being displayed as null */
		/*GameObject iconGO = new GameObject();
		SpriteRenderer iconSR = iconGO.AddComponent<SpriteRenderer>();
		iconGO.transform.parent = this.transform;
		iconGO.transform.position = cell.worldPos;

		if (cell.cost == 0)
		{
			iconSR.sprite = ffIcons[3];
			Quaternion newRot = Quaternion.Euler(0, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.cost == byte.MaxValue)
		{
			iconSR.sprite = ffIcons[2];
			Quaternion newRot = Quaternion.Euler(0, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.North)
		{
			iconSR.sprite = ffIcons[0];
			Quaternion newRot = Quaternion.Euler(0, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.South)
		{
			iconSR.sprite = ffIcons[0];
			Quaternion newRot = Quaternion.Euler(0, 0, 180);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.East)
		{
			iconSR.sprite = ffIcons[0];
			Quaternion newRot = Quaternion.Euler(0, 0, 270);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.West)
		{
			iconSR.sprite = ffIcons[0];
			Quaternion newRot = Quaternion.Euler(0, 0, 90);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.NorthEast)
		{
			iconSR.sprite = ffIcons[1];
			Quaternion newRot = Quaternion.Euler(0, 0, 0);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.NorthWest)
		{
			iconSR.sprite = ffIcons[1];
			Quaternion newRot = Quaternion.Euler(0, 0, 90);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.SouthEast)
		{
			iconSR.sprite = ffIcons[1];
			Quaternion newRot = Quaternion.Euler(0, 0, 270);
			iconGO.transform.rotation = newRot;
		}
		else if (cell.bestDirection == GridDirection.SouthWest)
		{
			iconSR.sprite = ffIcons[1];
			Quaternion newRot = Quaternion.Euler(0, 0, 180);
			iconGO.transform.rotation = newRot;
		}
		else
		{
			iconSR.sprite = ffIcons[0];
		}
	}

	public void ClearCellDisplay()
	{
		foreach (Transform t in transform)
		{
			GameObject.Destroy(t.gameObject);
		}
	}
	
	private void OnDrawGizmos()
	{
		if (displayGrid)
		{
			if (curFlowField == null)
			{
				DrawGrid(gridController.gridList, Color.yellow, gridController.cellRadius);
			}
		}
		
		if (curFlowField == null) { return; }

		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.alignment = TextAnchor.MiddleCenter;

		switch (curDisplayType)
		{
			case FlowFieldDisplayType.CostField:

				foreach (Cell curCell in curFlowField.grid)
				{
					Handles.Label(curCell.worldPos, curCell.cost.ToString(), style);
				}
				break;
				
			case FlowFieldDisplayType.IntegrationField:

				foreach (Cell curCell in curFlowField.grid)
				{
					Handles.Label(curCell.worldPos, curCell.bestCost.ToString(), style);
				}
				break;
				
			default:
				break;
		}
		
	}

	private void DrawGrid(List<Vector2Int> gridList, Color drawColor, float drawCellRadius)
	{
		Gizmos.color = drawColor;
		if (curFlowField == null) { return; }
		for(int i = 0; gridList.Count - 1 >= i; i++){
			for(int x = (gridList[i].x * 10); x < (gridList[i].x * 10) + 10; x++){
				for(int y = (gridList[i].y * 10); y < (gridList[i].y * 10) + 10; y++){
					Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, drawCellRadius * 2 * y + drawCellRadius, 0);
					Vector3 size = Vector2.one * drawCellRadius * 2;
					Gizmos.DrawWireCube(center, size);
				}
			}
		}
	}
}*/
