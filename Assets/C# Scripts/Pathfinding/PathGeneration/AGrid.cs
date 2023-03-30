using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AGrid : MonoBehaviour {

	public bool onlyDisplayPathGizmos;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		//Vector3 worldBottomLeft = transform.position + Vector3.right * gridWorldSize.x/2 + Vector3.forward * gridWorldSize.y/2;
		
		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				//Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				Vector3 worldPoint = new Vector3(nodeDiameter * x + nodeRadius, nodeDiameter * y + nodeRadius, 0);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = worldPosition.x / (gridSizeX * nodeDiameter);
		float percentY = worldPosition.y / (gridSizeY * nodeDiameter);
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		//Debug.Log(percentX + " " + percentY);

		int x = Mathf.Clamp(Mathf.FloorToInt((gridSizeX) * percentX), 0, gridSizeX - 1);
		int y = Mathf.Clamp(Mathf.FloorToInt((gridSizeY) * percentY), 0, gridSizeY - 1);

		//int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		//int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		//Debug.Log(x + " " + y);
		return grid[x,y];
	}

	public List<Node> path;
	public Node firstNode;
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,gridWorldSize.y,1));

		if (onlyDisplayPathGizmos) {
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.blue;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}
		else {

			if (grid != null) {
				foreach (Node n in grid) {
					Gizmos.color = (n.walkable)?Color.white:Color.red;
					if (path != null)
						if (path.Contains(n))
							Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}
	}
}