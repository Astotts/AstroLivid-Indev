using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMissileLauncher : WeaponClass
{
    [SerializeField] GameObject projectile;
    [SerializeField] Rigidbody2D rb;

    private float firingElapsed;
    [SerializeField] private float duration;
    
    // Update is called once per frame
    void Update()
    {
        if(target != null){
            firingElapsed += Time.deltaTime;
            if(firingElapsed >= duration && target != null){
                firingElapsed = 0;
                GameObject spawned = Instantiate(projectile, transform.position, transform.rotation);
                MissileMovement missileMovement = spawned.GetComponent<MissileMovement>();
                missileMovement.SetMissileTarget(target);
                missileMovement.rb.velocity = rb.velocity;
                missileMovement.layermask = transform.parent.GetComponent<TargetingSystem>().layermask;
                spawned.tag = this.gameObject.transform.parent.tag;
                spawned.layer = LayerMask.NameToLayer(LayerMask.LayerToName(this.gameObject.layer + 1));
            }
        }
    }
}