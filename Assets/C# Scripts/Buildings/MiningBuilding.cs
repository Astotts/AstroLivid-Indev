using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBuilding : MonoBehaviour
{
    private ResourceManager resourceManager;

    private Vector2 absorbVector;

    private float resourceTime;
    private float resourceStartTime = 3f;

    private float creditAmount = 1250f;

    public float creditCost;
    public float populationCost;
    public float powerCost;

    private BuildingIdentifyer buildingIdentifyer;

    void Start(){
        resourceManager = gameObject.GetComponentInParent<ResourceManager>();
        buildingIdentifyer = gameObject.GetComponent<BuildingIdentifyer>();
            resourceManager.credits -= creditCost;
            resourceManager.population -= populationCost;
            resourceManager.powerUsed += powerCost;        
    }

    void Update(){
                if(buildingIdentifyer.built){
            resourceTime += -1f * Time.deltaTime;

            if(resourceTime <= 0f){
                resourceTime = resourceStartTime;
                resourceManager.credits += creditAmount;
            }       
        }
    }
}