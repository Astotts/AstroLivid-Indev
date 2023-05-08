using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationBuilding : BuildingIdentifyer
{

    private float resourceTime;
    private float resourceStartTime = 15f;

    private float populationAmount = 1f;

    private float totalPopulation;

    void Start(){
        //resourceManager.population += 10f;
        //resourceManager.credits -= creditCost;
        //resourceManager.powerUsed += powerCost;
    }

    // Update is called once per frame
    void Update()
    {
        if(totalPopulation <= 25f){
            resourceTime += -1f * Time.deltaTime;

            if(resourceTime <= 0f){
                resourceTime = resourceStartTime;
                totalPopulation += populationAmount;
                //resourceManager.population += populationAmount;
            }
        }
        
        
    }


}
