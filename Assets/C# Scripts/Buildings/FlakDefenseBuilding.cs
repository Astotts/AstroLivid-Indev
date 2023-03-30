using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakDefenseBuilding : MonoBehaviour
{
    private ResourceManager resourceManager;

    [SerializeField] private GameObject flakTurret;

    public float creditCost;
    public float populationCost;
    public float powerCost;

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