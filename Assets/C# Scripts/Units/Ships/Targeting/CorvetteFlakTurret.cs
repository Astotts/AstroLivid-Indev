using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteFlakTurret : WeaponClass
{
    [SerializeField] GameObject projectile;

    private float firingElapsed;
    [SerializeField] private float duration;

    private float volleyTime = 0.2f;

    private int volleyProjectileCount = 2;

    //temp
    int i;
    float fireTime; 


    

    
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
                i = volleyProjectileCount;
                fireTime = volleyTime; 
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire(){
        while(i > 0){
            fireTime -= Time.deltaTime;
            if(fireTime <= 0f){
                GameObject spawned = Instantiate(projectile, transform.position, transform.rotation);
                spawned.tag = this.gameObject.tag;
                spawned.layer = LayerMask.NameToLayer(LayerMask.LayerToName(this.gameObject.layer + 1));
                ArtilleryTurretShell artilleryTurretShell = spawned.GetComponent<ArtilleryTurretShell>();
                artilleryTurretShell.layermask = transform.parent.parent.GetComponent<TargetingSystem>().layermask;
                artilleryTurretShell.tag = this.gameObject.transform.parent.tag;
                i--;
                fireTime = volleyTime;
                yield return null;
            }
        }
        yield break;
    }

    
}

