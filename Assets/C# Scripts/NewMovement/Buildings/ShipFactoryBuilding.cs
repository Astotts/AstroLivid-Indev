using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactoryBuilding : MonoBehaviour
{
    private ResourceManager resourceManager;

    public float creditCost;
    public float populationCost;
    public float powerCost;

    public float artilleryFighterCost;
    [SerializeField] private GameObject artilleryFighterPrefab;
    private GameObject artilleryFighter;

    void Start(){
        resourceManager = gameObject.GetComponentInParent<ResourceManager>();
        resourceManager.credits -= creditCost;
        resourceManager.population -= populationCost;
        resourceManager.powerUsed += powerCost;
    }

    public void ArtilleryFighterConstruct(){
        if(resourceManager.credits >= artilleryFighterCost){
            resourceManager.credits -= artilleryFighterCost;
            artilleryFighter = Instantiate(artilleryFighterPrefab, gameObject.transform.position, gameObject.transform.rotation, resourceManager.transform);
            artilleryFighter.tag = gameObject.tag;
            artilleryFighter.layer = 10;
        }
    }
}  
