using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuilding : MonoBehaviour
{
    private ResourceManager resourceManager;

    private float powerAmount = 8f;

    public float creditCost;
    public float populationCost;
    public float powerCost;

    void Start(){
        resourceManager = gameObject.GetComponentInParent<ResourceManager>();
        resourceManager.credits -= creditCost;
        resourceManager.population -= populationCost;
        resourceManager.powerUsed += powerCost;
        resourceManager.power += powerAmount;
    }
}
