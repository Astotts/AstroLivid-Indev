using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class GridController : MonoBehaviour
{
	[SerializeField] private Pathfinding pathfinding;
	[SerializeField] private Transform seeker;
    [SerializeField] private ShipSelector shipSelector;
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;
	public GridDebug gridDebug;
	private Stopwatch stopwatch;

	private Cell[,] grid;
    
    private List<Vector2Int> initialList;
    private List<UnitIdentifyer> unitList;

    private List<Node> overallPath;
    private HashSet<Vector2Int> tempGridList;

    public List<Vector2Int> gridList;

    private int indexX;
    private int indexY;

    private void InitializeFlowField()
	{
        curFlowField = new FlowField(cellRadius, gridSize);
		curFlowField.CreateGrid(gridList);
		gridDebug.SetFlowField(curFlowField);		

        /*The file should then queue up the creation of cost grids for each index as a threaded job
        then pass them along to the flowfield class*/
		curFlowField.CreateCostField(gridList);

        /*the flowfield class should then take the cost grids and create an integration grid then a
        vector grid*/

		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		Cell destinationCell = curFlowField.GetCellFromWorldPos(worldMousePos);
		curFlowField.CreateIntegrationField(destinationCell);

		curFlowField.CreateFlowField(gridList);

		gridDebug.DrawFlowField(gridList, curFlowField.grid);
        gridList.Clear();

        if(shipSelector.selectedUnitIdentifyerList != null && shipSelector.selectedUnitIdentifyerList[0] != null){
            foreach(UnitIdentifyer unit in shipSelector.selectedUnitIdentifyerList){    
                unit.GetComponent<Boid>().curFlowField = this.curFlowField;
            }
        }

		stopwatch.Stop();
		UnityEngine.Debug.Log(stopwatch.Elapsed.TotalMilliseconds);	
	}
    
    /*To-Do

    Work on non-cardinal grid directions
    
    Multithreading

    */


    /* This File needs to take the A* path and return the 
    grid Index's that the flowfield will need to iterate through*/
    public List<Vector2Int> HierarchicalSearch(){
        //curFlowField.GetCellFromWorldPos(seeker.position);
        indexX = -1;
        indexY = -1;
        int lastIndexX = -1;
        int lastIndexY = -1; 

        tempGridList = new HashSet<Vector2Int>();

        //Iterate for the rest of the grid spaces
        for(int i = 0; overallPath.Count > i; i++){
            indexX = Mathf.FloorToInt(overallPath[i].gridX/10);
            indexY = Mathf.FloorToInt(overallPath[i].gridY/10);
            if(indexX != lastIndexX || indexY != lastIndexY){
                tempGridList.Add(new Vector2Int(indexX,indexY));
                lastIndexX = indexX;
                lastIndexY = indexY;
            }
        }
        gridList = new List<Vector2Int>(tempGridList);

        if(gridList.Count > 2){
            for(int i = 0; gridList.Count - 1 > i; i++){
                lastIndexX = gridList[i].x;
                lastIndexY = gridList[i].y;
                indexX = gridList[i + 1].x;
                indexY = gridList[i + 1].y;
                if(indexX != lastIndexX && indexY != lastIndexY){
                    tempGridList.Add(new Vector2Int(indexX, lastIndexY));
                    tempGridList.Add(new Vector2Int(lastIndexX, indexY));
                }
            }
        }
        
        gridList.AddRange(tempGridList);

        return gridList;
    }

	void Update(){
		if(Input.GetMouseButtonDown(1)){
            stopwatch = new Stopwatch();
		    stopwatch.Start();
            overallPath = new List<Node>();
            initialList = new List<Vector2Int>();
            unitList = new List<UnitIdentifyer>();

            foreach(UnitIdentifyer unit in shipSelector.GetUnitList()){
                //Debug.Log(worldPos);
                float percentX = unit.transform.position.x / (gridSize.x * (cellRadius * 2));
                float percentY = unit.transform.position.y / (gridSize.y * (cellRadius * 2));

                percentX = Mathf.Clamp01(percentX);
                percentY = Mathf.Clamp01(percentY);

                int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
                int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);
                //Debug.Log(x + " " + y);
                indexX = Mathf.FloorToInt(y/10);
                indexY = Mathf.FloorToInt(x/10);
                Vector2Int testVector = new Vector2Int(indexX, indexY);
                
                if(initialList.Count == 0){initialList.Add(testVector); unitList.Add(unit);}
                if(!initialList.Contains(testVector)){
                    initialList.Add(testVector);
                    unitList.Add(unit);
                }
            }
            foreach(UnitIdentifyer unit in unitList){
                //Change out seeker.position for each individually saved unit in a gridIndex
                pathfinding.FindPath(unit.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)));
                overallPath.AddRange(pathfinding.grid.path);
                overallPath.Add(pathfinding.grid.firstNode);
            }

            HierarchicalSearch();
            InitializeFlowField();
		}
	}


}
