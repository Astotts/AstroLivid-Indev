using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMissileLauncher : WeaponClass
{
    [SerializeField] GameObject projectile;

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
                spawned.GetComponent<MissileMovement>().SetMissileTarget(target);
                spawned.tag = this.gameObject.tag;
                spawned.layer = this.gameObject.layer;
            }
        }
    }
}