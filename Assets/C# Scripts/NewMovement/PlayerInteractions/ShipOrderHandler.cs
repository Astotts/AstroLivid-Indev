/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOrderHandler : MonoBehaviour
{
    private UnitIdentifyer unit;

    public GameObject gridController;

    private GridController gridControllerComponent;

    public Order shipOrder;

    private /*FlowFieldNS*//*FlowField curFlowField;

    private Collider2D attackCollider;

    private List<Order> orderList;

    private int actionIndex = -1;

    public bool actionCompleted;

    void Start()
    {
        unit = gameObject.GetComponent<UnitIdentifyer>();
        gridControllerComponent = gridController.GetComponent<GridController>();
    }

    public void SetUpOrderList(Order _shipOrder){
        if(Input.GetAxisRaw("LeftShift") > 0){
            Debug.Log(orderList.Count);
            //Debug.Log("orderList action added");
            if(orderList == null){
                actionIndex = -1;
                orderList = new List<Order>();
                //Debug.Log("SetMap");
                unit.SetMap(_shipOrder.pathfindingMap);
                actionIndex++;
            }
            orderList.Add(_shipOrder);
            Debug.Log(orderList.Count);
        }
        else{
            //Debug.Log("reset orderList");
            orderList = null;
            orderList = new List<Order>();
            actionIndex = -1;
            orderList.Add(_shipOrder);
            unit.SetMap(_shipOrder.pathfindingMap);
            //Debug.Log("SetMap");
            actionIndex++;
        }
    }

    void Update()
    {
        if(orderList != null && orderList.Count > 0 && actionCompleted){
            actionCompleted = false;
            if(orderList.Count - 1 > actionIndex){
                actionIndex++;
                if(orderList[actionIndex].attackCollider != null){
                    //unit.Attack(attackCollider);
                }
                else{
                    unit.SetMap(orderList[actionIndex].pathfindingMap);
                    //Debug.Log("SetMap");
                }
            }
        }
    }
}*/
