using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBuilding : BuildingIdentifyer
{
    private Vector2 absorbVector;

    private float resourceTime;
    private float resourceStartTime = 3f;

    private float creditAmount = 1250f;

    void Start(){
        //resourceManager = gameObject.GetComponentInParent<ResourceManager>();
            ResourceManager.credits -= creditCost;
            ResourceManager.population -= populationCost;
            ResourceManager.powerUsed += powerCost;        
    }

    void Update(){
        /*        if(buildingIdentifyer.built){
            resourceTime += -1f * Time.deltaTime;

            if(resourceTime <= 0f){
                resourceTime = resourceStartTime;
                resourceManager.credits += creditAmount;
            }       
        }*/
    }
}