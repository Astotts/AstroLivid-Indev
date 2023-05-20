using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : UnitIdentifyer
{   
    [SerializeField] private List<GameObject> buildingList;
    private List<GameObject> piecesList;
    private List<Transform> positionList;
    private List<BuildOrder> buildQueue;
    [SerializeField] private List<ConstructionShip> constructionShips;
    
    
    // Start is called before the first frame update
    void Start()
    {
        buildQueue = new List<BuildOrder>();
        positionList = new List<Transform>();
        piecesList = new List<GameObject>();
    }

    public void BuildStructure(int index, Vector3 pos, Quaternion rot){
        GameObject building = Instantiate(buildingList[index], pos, rot);
        BuildingIdentifyer buildingID = building.GetComponent<BuildingIdentifyer>();
        buildQueue.Add(buildingID.buildOrder);
        building.tag = this.gameObject.tag;
        building.layer = this.gameObject.layer;
    }

    void Update(){
        foreach(ConstructionShip constructionShip in constructionShips){
            //if building is canceled update buildorder with remaining pieces and remove buildorder (make sure ships currently building finish their state)
            switch (constructionShip.constructionState)
            {
                case ConstructionStates.Available:
                    piecesList.AddRange(buildQueue[0].piecesList);
                    positionList.AddRange(buildQueue[0].positionList); 
                    constructionShip.constructionState = ConstructionStates.Awaiting;
                break;

                case ConstructionStates.Awaiting:
                    if(piecesList.Count == 0){
                        constructionShip.constructionState = ConstructionStates.Available;
                        buildQueue.RemoveAt(0);
                        break;
                    }
                    constructionShip.SetPiece(piecesList[0]);
                    constructionShip.destination = positionList[0];
                    positionList.RemoveAt(0);
                    piecesList.RemoveAt(0);
                    constructionShip.constructionState = ConstructionStates.Building;
                break;

                case ConstructionStates.Done:
                    constructionShip.constructionState = ConstructionStates.Returning;
                break;

                case ConstructionStates.Returning:
                    if(Vector2.Distance(constructionShip.transform.position, constructionShip.home.position) < 5f){
                        constructionShip.constructionState = ConstructionStates.Awaiting;
                        break;
                    }
                break;

            }
        }
        
    }

    //Need Interface between UI and Queue
    public void CancelStructure(){
        //buildQueue.Remove();
    }
}