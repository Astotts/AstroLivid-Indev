using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIdentifyer : UnitIdentifyer
{
    public float creditCost;
    public float populationCost;
    public float powerCost;

    private float increasedTimer = 0.5f;
    
    private float healthResetTimer;
    private float normalTimer = 0.3f;
    private float healthTimer;
    public bool built;

    void Awake()
    {
        healthManager = gameObject.GetComponent<HealthManager>();
        SetSelectedVisible(false);
        SetHealthVisible(false);
        built = false;
    }

    void FixedUpdate(){
        /*if(resourceManagerScript.powerUsed > resourceManagerScript.power){
            healthResetTimer = increasedTimer;
        }
        else{
            healthResetTimer = normalTimer;
        }
        
        if(healthManager.maxHealth > healthManager.health){
            healthTimer += 1f * Time.deltaTime;
        }
        else{
            built = true;
        }
        
        if(!built && healthTimer >= healthResetTimer){
            healthManager.health += 1;
            healthTimer = 0f;
        }*/
    }
}
