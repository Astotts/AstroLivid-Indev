/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class OrderGenerator : MonoBehaviour
{
    [SerializeField] private Transform attackAreaTransform;

    [SerializeField] private GridController gridController;

    private Pathfinding astarPathFind;

    //PathFindingInstructions pathFindingMap;

    [SerializeField] private GameObject aStarObject;

    private Collider2D colliderAttack;

    private /*FlowFieldNS*//*FlowField curFlowField;

    private List<List</*FlowFieldNS*//*FlowField flowFieldList;

    List<Collider2D> selectedShipArray;

    private List<UnitIdentifyer> shipSelectorList;

    private List<TargetingSystem> targetingSystemList;

    [SerializeField] private ShipSelector shipSelector;

    public List<Node> path;

    [SerializeField] private Pathfinding pathfinder;

    private /*FlowFieldNS*//*FlowField destinationFlowfield;

    void Start()
    {
        attackAreaTransform.gameObject.SetActive(false);
        astarPathFind = aStarObject.GetComponent<Pathfinding>();
    }

    public void SetArray(List<Collider2D> selectedShipArray){
        this.selectedShipArray = selectedShipArray;
    }
    
    void Update()
    {   
        if(Input.GetMouseButtonDown(1) && selectedShipArray.Count > 0){
            attackAreaTransform.gameObject.SetActive(true);
            attackAreaTransform.position = UtilsClass.GetMouseWorldPosition();
            colliderAttack = Physics2D.OverlapArea(UtilsClass.GetMouseWorldPosition(), UtilsClass.GetMouseWorldPosition());
            attackAreaTransform.gameObject.SetActive(false);
            //path = astarPathFind.FindPath(UtilsClass.GetMouseWorldPosition(), selectedShipArray[0].transform.position);
            //pathfinder.FindPath(Cell, selectedShipArray[0]);
            GenerateOrder();
        }
    }

    /*public void RecalculatePath(UnitIdentifyer ship, Vector3 prevDestPos){
        path = astarPathFind.FindPath(prevDestPos, ship.transform.position);
        PathFindingInstructions pathFindingMap = new PathFindingInstructions(path, gridController);
        ship.SetMap(pathFindingMap);
    }*//*

    void GenerateOrder(){
        //PathFindingInstructions pathFindingMap = new PathFindingInstructions(path, gridController, UtilsClass.GetMouseWorldPosition());
        if(selectedShipArray != null && path != null && path.Count > 0){
            foreach(Collider2D ship in selectedShipArray){
                if(ship != null && ship.tag != "Asteroid"){
                    Order shipOrder = new Order(colliderAttack, pathFindingMap);
                    ship.GetComponent<ShipOrderHandler>().SetUpOrderList(shipOrder);
                    //Debug.Log("Order Generated");
                }
            }
        }
    }
}*/