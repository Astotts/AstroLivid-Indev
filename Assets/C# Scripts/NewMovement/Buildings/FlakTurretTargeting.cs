/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakTurretTargeting : MonoBehaviour
{   
    public GameObject turretBase;
    private float artilleryTime = 0.2f;
    private float currentArtilleryTime;

    private Collider2D enemy;    

    private GameObject artilleryFighter;

    private BuildingIdentifyer buildingIdentifyer;

    void Start()
    {   
        artilleryFighter = this.gameObject;

        currentArtilleryTime = artilleryTime;

        buildingIdentifyer = gameObject.GetComponent<BuildingIdentifyer>();
    }

    public void SetTarget(Collider2D enemy){
        this.enemy = enemy;
    }

    void Update()
    {
        if(buildingIdentifyer.built){
            Vector2 position = transform.position;

            currentArtilleryTime -= 1 * Time.deltaTime;

            if(enemy == null){
                ArtilleryTurretTracking turretTracking = turretBase.GetComponent<ArtilleryTurretTracking>();
                turretTracking.SetTurretTarget(null);
            }
            else if(enemy != null) {
                if(currentArtilleryTime <= 0){
                    currentArtilleryTime = artilleryTime;
                    ArtilleryTurretTracking turretTracking = turretBase.GetComponent<ArtilleryTurretTracking>();
                    turretTracking.SetTurretTarget(enemy);
                    turretTracking.GiveParent(gameObject);
                }
            }
        }
        
    }
}
*/