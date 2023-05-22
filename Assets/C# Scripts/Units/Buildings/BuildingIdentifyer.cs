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
    
    //Construction
    public bool built;
    [SerializeField] private List<GameObject> piecesList;
    [SerializeField] private List<Transform> positionList;
    public BuildOrder buildOrder;
    [SerializeField] private int partCount;
    private int placedCount;

    private List<Collider2D> physicsCheck;

    //[SerializeField] private ContactFilter2D physicsCheck;

    [SerializeField] private GameObject structure;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        buildOrder = new BuildOrder{type = variant, building = this, piecesList = this.piecesList, positionList = this.positionList};
        //healthManager = gameObject.GetComponent<HealthManager>();
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

    public void PlacePart(){
        healthManager.health += healthManager.maxHealth / partCount;
        piecesList.RemoveAt(0);
        positionList.RemoveAt(0);
        if(placedCount == partCount){
            spriteRenderer.enabled = !spriteRenderer.enabled;
            structure.SetActive(true);
        }
        placedCount++;
    }
}
