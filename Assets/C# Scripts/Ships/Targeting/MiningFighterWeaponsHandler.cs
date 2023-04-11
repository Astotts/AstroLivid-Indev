using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class MiningFighterWeaponsHandler : WeaponClass
{   
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private GameObject turretBaseOne;
    [SerializeField] private GameObject turretBaseTwo;

    [SerializeField] private GameObject drawBeamPointOne;
    [SerializeField] private GameObject drawBeamPointTwo;

    private DrawMiningBeam miningBeamOne;
    private DrawMiningBeam miningBeamTwo;

    private ShipSteering steering;
    private float currentTime;

    public Collider2D asteroid;
    public Collider2D savedAsteroid;

    public bool asteroidTargeted;
    public bool asteroidConfirmed;

    public Vector3 depositPos;
    private Vector3 savedDepositPos;

    void Start(){
        miningBeamOne = drawBeamPointOne.GetComponent<DrawMiningBeam>();
        miningBeamTwo = drawBeamPointTwo.GetComponent<DrawMiningBeam>();
        steering = gameObject.GetComponent<ShipSteering>();
    }

    void Update()
    {
        /*if(asteroid != null && asteroid.GetComponent<AsteroidIdentifyer>()){
            //Debug.Log(depositPos);
            if(Vector3.Distance(gameObject.transform.position, asteroid.transform.position) <= 35f && depositPos != Vector3.zero && asteroidTargeted == false){
                asteroid.gameObject.layer = 7;
                //asteroid.GetComponent<AsteroidIdentifyer>().CreateFlowField(depositPos);
                savedDepositPos = depositPos;
                savedAsteroid = asteroid;
                AsteroidController.SetAsteroidList(asteroid);
                asteroidTargeted = true;
                //Debug.Log("Set");
            }
            if(savedDepositPos != depositPos){
                //asteroid.GetComponent<AsteroidIdentifyer>().CreateFlowField(depositPos);
            }
            else if(asteroid.gameObject.layer != 8 && Vector3.Distance(gameObject.transform.position, asteroid.transform.position) > 35f){
                if(!asteroid.GetComponent<AsteroidIdentifyer>().grabbed){
                    asteroid.gameObject.layer = LayerMask.NameToLayer("Impassible");
                }
                asteroid.GetComponent<AsteroidIdentifyer>().curFlowField = null;
                asteroidTargeted = false;
                //Debug.Log("Null");
            }
        }
        if(asteroid == null || (asteroid != savedAsteroid && asteroidTargeted == true)){
            asteroidTargeted = false;
        }

        if(asteroid && savedAsteroid && asteroid == savedAsteroid && asteroidTargeted){
            asteroidConfirmed = true;
        }
        else{
            asteroidConfirmed = false;
        }
        currentTime -= 1 * Time.deltaTime;
    }

    void FixedUpdate(){
        if(savedAsteroid != null && savedAsteroid == asteroid){
            //Debug.Log(asteroid);
            steering.asteroid = savedAsteroid;
            miningBeamOne.p2 = asteroid.transform;
            miningBeamTwo.p2 = asteroid.transform;
        }
        else{
            steering.asteroid = null;
            miningBeamOne.p2 = null;
            miningBeamTwo.p2 = null;
        }*/
    }
}