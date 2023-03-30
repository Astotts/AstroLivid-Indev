using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class UIButtonManager : MonoBehaviour
{
    private Vector3 mousePos;

    [SerializeField] private GameObject resourceManager;

    private ResourceManager resourceManagerScript;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Color green;

    [SerializeField] private Color red;

    private Collider2D[] asteroidArray;

    private Collider2D[] buildingCheckArray;

    private Collider2D buildOutlineCollider;
    [SerializeField] private ContactFilter2D contactFilter;
    private Collider2D[] results;

    [SerializeField] private GameObject buildOutline;

    [SerializeField] private GameObject buildButtons;

    [SerializeField] private GameObject shipBuildButtons;

    public LayerMask asteroidMask;

    public LayerMask buildingMask;

    public GameObject miningBuilding;
    private MiningBuilding miningScript;

    //public GameObject harversterBuilding;
    public GameObject powerBuilding;
    private PowerBuilding powerScript;

    public GameObject populationBuilding;
    private PopulationBuilding populationScript;

    public GameObject shipFactoryBuilding;

    private ShipFactoryBuilding shipFactoryScript;

    public GameObject flakBuilding;
    private FlakDefenseBuilding flakScript;

    public GameObject harvesterBuilding;
    private HarvesterBuilding harvesterScript;

    private GameObject building;

    private float creditCost;
    private float populationCost;
    private float powerCost;

    private bool spaceAvailable;

    private int caseNumber;

    [SerializeField] private GameObject buildingSelector;
    private ShipSelector shipSelector;
    private List<BuildingIdentifyer> selectedArray;
    private List<ShipFactoryBuilding> factoryArray;

    void Start(){
        results = new Collider2D[1];
        buildOutline.SetActive(false);
        spriteRenderer = buildOutline.GetComponent<SpriteRenderer>();
        resourceManagerScript = resourceManager.GetComponent<ResourceManager>();
        buildOutlineCollider = buildOutline.GetComponent<Collider2D>();
        shipSelector = buildingSelector.GetComponent<ShipSelector>();
    }
    void Update()
    {
        if(buildOutline.activeInHierarchy){
            mousePos = UtilsClass.GetMouseWorldPosition();
            if(Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(1)){
                buildOutline.SetActive(false);
            }

            buildOutline.transform.position = mousePos;
            
            if(building){
                buildOutline.transform.localScale = building.transform.localScale;
            }

            if(buildOutlineCollider.OverlapCollider(contactFilter, results) > 0){
                spriteRenderer.color = red;
                spaceAvailable = false;
            }
            else{
                spaceAvailable = true;
                spriteRenderer.color = green;
            }

            if(Input.GetMouseButtonDown(0) && spaceAvailable && resourceManagerScript.credits >= creditCost && resourceManagerScript.population >= populationCost){
                Debug.Log("Instantiated");
                Instantiate(building, buildOutline.transform.position, buildOutline.transform.rotation, resourceManager.transform); 
            }
        }
    }

    public void ShipFactoryUI(bool enabled){
        if(enabled){
            buildButtons.SetActive(!enabled);
            shipBuildButtons.SetActive(enabled);
        }
        else{
            buildButtons.SetActive(!enabled);
            shipBuildButtons.SetActive(enabled);
        }
    }

    public void MiningBuilding(){
        building = miningBuilding;
        miningScript = building.GetComponent<MiningBuilding>();
        this.creditCost = miningScript.creditCost;
        this.powerCost = miningScript.powerCost;
        this.populationCost = miningScript.populationCost;
        buildOutline.SetActive(true);
    }

    public void PowerBuilding(){
        building = powerBuilding;
        powerScript = building.GetComponent<PowerBuilding>();
        this.creditCost = powerScript.creditCost;
        this.powerCost = powerScript.powerCost;
        this.populationCost = powerScript.populationCost;
        buildOutline.SetActive(true);
    }

    public void PopulationBuilding(){
        building = populationBuilding;
        populationScript = building.GetComponent<PopulationBuilding>();
        this.creditCost = populationScript.creditCost;
        this.powerCost = populationScript.powerCost;
        this.populationCost = populationScript.populationCost;
        buildOutline.SetActive(true);
    }

    public void FlakTurretBuilding(){
        building = flakBuilding;
        flakScript = building.GetComponent<FlakDefenseBuilding>();
        this.creditCost = flakScript.creditCost;
        this.powerCost = flakScript.powerCost;
        this.populationCost = flakScript.populationCost;
        buildOutline.SetActive(true);
    }

    public void HarvesterBuilding(){
        building = harvesterBuilding;
        harvesterScript = building.GetComponent<HarvesterBuilding>();
        this.creditCost = harvesterScript.creditCost;
        this.powerCost = harvesterScript.powerCost;
        this.populationCost = harvesterScript.populationCost;
        buildOutline.SetActive(true);
    }

    public void ShipFactoryBuilding(){
        building = shipFactoryBuilding;
        shipFactoryScript = building.GetComponent<ShipFactoryBuilding>();
        this.creditCost = shipFactoryScript.creditCost;
        this.powerCost = shipFactoryScript.powerCost;
        this.populationCost = shipFactoryScript.populationCost;
        buildOutline.SetActive(true);
    }

    public void BuildArtilleryShip(){
        selectedArray = new List<BuildingIdentifyer>();
        selectedArray = shipSelector.GetBuildingList();
        factoryArray = new List<ShipFactoryBuilding>();
        foreach(BuildingIdentifyer buildingIdentifyer in selectedArray){
            if(buildingIdentifyer.GetComponentInParent<ShipFactoryBuilding>()){
                factoryArray.Add(buildingIdentifyer.GetComponentInParent<ShipFactoryBuilding>());
            }
        }
        foreach(ShipFactoryBuilding shipFactoryBuilding in factoryArray){
            shipFactoryBuilding.ArtilleryFighterConstruct();
        }
    }
}
