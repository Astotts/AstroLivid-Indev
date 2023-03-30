using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIdentifyer : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameObject;

    [SerializeField] private GameObject selectedHealthObject;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teamHandler;

    [SerializeField] private GameObject resourceManager;
    private TeamHandler teamHandlerScript;

    private HealthManager healthManager;

    private ResourceManager resourceManagerScript;

    private float increasedTimer = 0.5f;
    
    private float healthResetTimer;
    private float normalTimer = 0.3f;
    private float healthTimer;
    public bool built;

    void Start()
    {
        gameObject.tag = gameObject.transform.parent.tag;
        teamHandlerScript = teamHandler.GetComponent<TeamHandler>();
        resourceManagerScript = resourceManager.GetComponent<ResourceManager>();
        healthManager = gameObject.GetComponent<HealthManager>();
        teamHandlerScript.SetUpInstatiatedObject(gameObject, true);
        SetSelectedVisible(false);
        SetHealthVisible(false);
        built = false;
    }

    void FixedUpdate(){
        if(gameObject.layer == 6){
            teamHandlerScript.SetUpInstatiatedObject(gameObject, true);
        }

        if(resourceManagerScript.powerUsed > resourceManagerScript.power){
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
        }
    }

    public void SetSelectedVisible(bool visible){
        if(selectedGameObject != null){
        selectedGameObject.SetActive(visible);
        }
    }
    public void SetHealthVisible(bool visible){
        if(selectedHealthObject != null){
        selectedHealthObject.SetActive(visible);
        }
    }
    
    public bool GetSelectedVisible(){
        return selectedGameObject.activeInHierarchy;
    }
}
