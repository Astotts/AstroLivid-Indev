using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakCannon : WeaponClass
{
    [SerializeField] GameObject projectile;

    private float firingElapsed;
    [SerializeField] private float duration;

    [SerializeField] float rotateSpeed = 400f;
    [SerializeField] bool rotationClamped;

    

    
    // Update is called once per frame
    void Update()
    {
        if(target != null){
            Vector3 look = transform.InverseTransformPoint(target.transform.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            
            transform.Rotate(0, 0, angle);
            

            firingElapsed += Time.deltaTime;
            if(firingElapsed >= duration && target != null){
                firingElapsed = 0;
                GameObject spawned = Instantiate(projectile, transform.position, transform.rotation);
                spawned.tag = this.gameObject.tag;
                spawned.layer = this.gameObject.layer;
                ArtilleryTurretShell artilleryTurretShell = spawned.GetComponent<ArtilleryTurretShell>();
                artilleryTurretShell.tag = this.tag;

                /*
                GameObject spawned = Instantiate(missile, transform.position, transform.rotation) as GameObject;
                MissileMovement missileMovement = spawned.GetComponent<MissileMovement>();
                missileMovement.SetUpTags(artilleryFighter);
                missileMovement.SetMissileTarget(enemy.GetComponent<Collider2D>());
                */
            }
        }
    }
}
