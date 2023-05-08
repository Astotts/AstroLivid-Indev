using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakDefenseBuilding : BuildingIdentifyer
{

    [SerializeField] private GameObject flakTurret;

    /*void Start(){
        resourceManager = gameObject.GetComponentInParent<ResourceManager>();
            resourceManager.credits -= creditCost;
            resourceManager.population -= populationCost;
            resourceManager.powerUsed += powerCost;        
    }

    void FixedUpdate(){
        if(resourceManager.powerUsed > resourceManager.power){
            flakTurret.GetComponent<ArtilleryTurretTracking>().enabled = false;
        }
        else{
            flakTurret.GetComponent<ArtilleryTurretTracking>().enabled = true;
        }
    }*/
}