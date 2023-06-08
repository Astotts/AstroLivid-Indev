using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : UnitIdentifyer
{   
    [SerializeField] private List<GameObject> buildingList;
    private List<GameObject> piecesList;
    private List<Transform> positionList;
    private List<BuildOrder> buildQueue;
    private List<BuildOrder> weldQueue;
    [SerializeField] private List<ConstructionShip> constructionShips;
    [SerializeField] private List<WeldingShip> weldingShips;
    
    
    // Start is called before the first frame update
    void Start()
    {
        buildQueue = new List<BuildOrder>();
        weldQueue = new List<BuildOrder>();
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
                    //Debug.Log("Available");
                    if(buildQueue.Count > 0 && piecesList.Count == 0){
                        constructionShip.building = buildQueue[0].building;
                        piecesList.AddRange(buildQueue[0].piecesList);
                        positionList.AddRange(buildQueue[0].positionList); 
                        break;
                    }
                    if(piecesList.Count > 0){
                        constructionShip.constructionState = ConstructionStates.Awaiting;
                        constructionShip.building = buildQueue[0].building;
                    }
                break;

                case ConstructionStates.Awaiting:
                    //Debug.Log("Awaiting");
                    if(piecesList.Count == 0){
                        constructionShip.constructionState = ConstructionStates.Available;
                        break;
                    }
                    if(!constructionShip.carrying && piecesList.Count > 0){
                        constructionShip.SetPiece(piecesList[0], buildQueue[0].building);
                        constructionShip.assignedDestination = positionList[0];
                        //Debug.Log(constructionShip.destination);
                        positionList.RemoveAt(0);
                        piecesList.RemoveAt(0);
                        constructionShip.constructionState = ConstructionStates.Grabbing;
                    }
                break;

                case ConstructionStates.Done:
                    //Debug.Log("Done");
                    constructionShip.constructionState = ConstructionStates.Returning;
                    
                break;

                case ConstructionStates.Returning:
                    //Debug.Log("Returning");
                    if(Vector2.Distance(constructionShip.transform.position, constructionShip.home.position) < 5f){
                        constructionShip.constructionState = ConstructionStates.Awaiting;
                        break;
                    }
                break;

                case ConstructionStates.Grabbing:
                    //Debug.Log("Building Handle This State In Construction Ship");
                    if(positionList.Count > 0 && constructionShip.carrying){
                        constructionShip.constructionState = ConstructionStates.Building;
                    }
                        
                    //constructionShip.constructionState = ConstructionStates.Done;
                break;

            }
        }
        if(buildQueue.Count > 0 && piecesList.Count == 0){
            weldQueue.Add(buildQueue[0]);
            buildQueue.RemoveAt(0);
        }

        foreach(WeldingShip weldingShip in weldingShips){
            //if building is canceled update buildorder with remaining pieces and remove buildorder (make sure ships currently building finish their state)
            switch (weldingShip.constructionState)
            {
                case ConstructionStates.Available:
                    //Debug.Log("Available");
                    if(weldQueue.Count > 0 && weldingShip.home == weldingShip.destination && weldQueue[0].building.built == true){
                        weldingShip.building = weldQueue[0].building;
                        weldingShip.assignedDestination = weldQueue[0].building.transform;
                        weldingShip.constructionState = ConstructionStates.Building;
                        weldingShip.SetWeld();
                        break;
                    }
                break;

                case ConstructionStates.Done:
                    //Debug.Log("Done");
                    weldingShip.constructionState = ConstructionStates.Returning;
                    weldQueue.RemoveAt(0);
                break;

                case ConstructionStates.Returning:
                    //Debug.Log("Returning");
                    if(Vector2.Distance(weldingShip.transform.position, weldingShip.home.position) < 5f){
                        weldingShip.constructionState = ConstructionStates.Available;
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