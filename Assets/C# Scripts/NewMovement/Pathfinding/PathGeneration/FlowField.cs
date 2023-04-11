using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
	public Cell[,] grid { get; set; }
	public Vector2Int gridSize { get; private set; }
	public float cellRadius { get; private set; }
	public Cell destinationCell;
	private float cellDiameter;

	public FlowField(float _cellRadius, Vector2Int _gridSize)
	{
		cellRadius = _cellRadius;
		cellDiameter = cellRadius * 2f;
		gridSize = _gridSize;
	}

	public void CreateCostField(List<Vector2Int> gridList)
	{
		Vector3 cellHalfExtents = Vector3.one * cellRadius;
		int terrainMask = LayerMask.GetMask("Impassible", "RoughTerrain");

		for(int i = 0; gridList.Count - 1 >= i; i++){
			//Debug.Log(i);
			for (int x = (gridList[i].x * 10); x < (gridList[i].x * 10) + 10; x++)
			{
				for (int y = (gridList[i].y * 10); y < (gridList[i].y * 10) + 10; y++)
				{
					Collider[] obstacles = Physics.OverlapBox(grid[x,y].worldPos, cellHalfExtents, Quaternion.identity, terrainMask);
					bool hasIncreasedCost = false;
					foreach (Collider col in obstacles)
					{
						if (col.gameObject.layer == 8)
						{
							grid[x,y].IncreaseCost(255);
							continue;
						}
						else if (!hasIncreasedCost && col.gameObject.layer == 9)
						{
							grid[x,y].IncreaseCost(3);
							hasIncreasedCost = true;
						}
					}
				}
			}
		}
	}

	public void CreateGrid(List<Vector2Int> gridList){
		grid = new Cell[gridSize.x, gridSize.y];
		for(int i = 0; gridList.Count - 1 >= i; i++){
			for (int x = (gridList[i].x * 10); x < (gridList[i].x * 10) + 10; x++)
			{
				for (int y = (gridList[i].y * 10); y < (gridList[i].y * 10) + 10; y++)
				{
					Vector3 worldPos = new Vector3(cellDiameter * x + cellRadius, cellDiameter * y + cellRadius, 0);
					grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
					//Debug.Log(grid[110,50].gridIndex);
					/* grid[110, 50] exists here but not above in the CreateCostField() function? */
				}
			}
		}
	}

	public void CreateIntegrationField(Cell _destinationCell)
	{
		destinationCell = _destinationCell;

		destinationCell.cost = 0;
		destinationCell.bestCost = 0;

		Queue<Cell> cellsToCheck = new Queue<Cell>();

		cellsToCheck.Enqueue(destinationCell);

		while(cellsToCheck.Count > 0)
		{
			Cell curCell = cellsToCheck.Dequeue();
			List<Cell> curNeighbors = GetNeighborCells(curCell.gridIndex, GridDirection.CardinalDirections);
			foreach (Cell curNeighbor in curNeighbors)
			{
				if (curNeighbor.cost == byte.MaxValue) { continue; }
				if (curNeighbor.cost + curCell.bestCost < curNeighbor.bestCost)
				{
					curNeighbor.bestCost = (ushort)(curNeighbor.cost + curCell.bestCost);
					cellsToCheck.Enqueue(curNeighbor);
				}
			}
		}
	}

	public void CreateFlowField(List<Vector2Int> gridList)
	{
		/*Replace the for each with a for to check over just the valid cells and find
		how to not check outside of the valid indexes*/
		for(int i = 0; gridList.Count - 1 >= i; i++){
			for(int x = (gridList[i].x * 10); x < (gridList[i].x * 10) + 10; x++){
				for(int y = (gridList[i].y * 10); y < (gridList[i].y * 10) + 10; y++){
					List<Cell> curNeighbors = GetNeighborCells(new Vector2Int(x, y), GridDirection.AllDirections);

					int bestCost = grid[x, y].bestCost;

					foreach(Cell curNeighbor in curNeighbors)
					{
						if(curNeighbor.bestCost < bestCost)
						{
							bestCost = curNeighbor.bestCost;
							/*Change directions to have more variation*/
							grid[x, y].bestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.gridIndex - grid[x, y].gridIndex);
						}
					}
				}
			}
		}		
	}

	private List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
	{
		List<Cell> neighborCells = new List<Cell>();

		foreach (Vector2Int curDirection in directions)
		{
			Cell newNeighbor = GetCellAtRelativePos(nodeIndex, curDirection);
			if (newNeighbor != null)
			{
				neighborCells.Add(newNeighbor);
			}
		}
		return neighborCells;
	}

	private Cell GetCellAtRelativePos(Vector2Int orignPos, Vector2Int relativePos)
	{
		Vector2Int finalPos = orignPos + relativePos;

		if (finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y)
		{
			return null;
		}

		else { return grid[finalPos.x, finalPos.y]; }
	}

	public Cell GetCellFromWorldPos(Vector3 worldPos)
	{
		//Debug.Log(worldPos);
		float percentX = worldPos.x / (gridSize.x * cellDiameter);
		float percentY = worldPos.y / (gridSize.y * cellDiameter);

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
		int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);
		//Debug.Log(x + " " + y);
		return grid[x, y];
	}
}
