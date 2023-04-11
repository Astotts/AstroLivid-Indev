using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterBuilding : MonoBehaviour{
    private ResourceManager resourceManager;
    private float targetingTime = 0.2f;
    private float currentTargetingTime = 0f;
    private Vector2 absorbVector;
    private float creditAmount = 1250f;

    [SerializeField] private LayerMask layerMask;

    private float range = 4f;
    public float creditCost;
    public float populationCost;
    public float powerCost;

    private BuildingIdentifyer buildingIdentifyer;

    void Start(){
        currentTargetingTime = targetingTime;
        resourceManager = gameObject.GetComponentInParent<ResourceManager>();
        buildingIdentifyer = gameObject.GetComponent<BuildingIdentifyer>();
            resourceManager.credits -= creditCost;
            resourceManager.population -= populationCost;
            resourceManager.powerUsed += powerCost;        
    }

    // Update is called once per frame
    void Update()
    {
        currentTargetingTime -= 1 * Time.deltaTime;
        if(currentTargetingTime <= 0f){
        currentTargetingTime = targetingTime;
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(this.gameObject.transform.position + (gameObject.transform.right * 7f), range);
        /*foreach(Collider2D collider in colliderArray){
            if(collider.gameObject.tag == "Asteroid"){
                absorbVector = collider.gameObject.transform.position - gameObject.transform.position;
                collider.GetComponent<AsteroidIdentifyer>().grabbed = true;
            if(absorbVector != Vector2.zero && collider.gameObject.tag == "Asteroid"){
                collider.gameObject.GetComponent<Rigidbody2D>().velocity -= absorbVector * (collider.gameObject.GetComponent<AsteroidIdentifyer>().moveSpeed/2) * Time.deltaTime;
                collider.gameObject.layer = 7;
                if(Vector3.Distance(collider.gameObject.transform.position, gameObject.transform.position) <= 1.5f){
                    Destroy(collider.gameObject);
                    resourceManager.credits += creditAmount;
                }
            }
            absorbVector = Vector2.zero;
            }
        }*/
        }
    }
}
