using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakCannon : WeaponClass
{
    [SerializeField] GameObject projectile;

    private float firingElapsed;
    [SerializeField] private float duration;

    [SerializeField] bool rotationClamped;

    public bool startAnim = false;
    public bool stopAnim = false;

    [SerializeField] Animator shellEjection;
    [SerializeField] ParticleSystem ejectedShell;


    

    
    // Update is called once per frame
    void Update()
    {
        if(!startAnim){
            stopAnim = false;
        }

        if(startAnim && !stopAnim){
            stopAnim = true;
            ejectedShell.Play();
        }

        if(target != null){
            Vector3 look = transform.InverseTransformPoint(target.transform.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            
            transform.Rotate(0, 0, angle);
            

            firingElapsed += Time.deltaTime;
            if(firingElapsed >= duration && target != null){
                firingElapsed = 0;
                GameObject spawned = Instantiate(projectile, transform.position, transform.rotation);
                shellEjection.SetTrigger("Fire");
                spawned.tag = this.gameObject.tag;
                spawned.layer = LayerMask.NameToLayer(LayerMask.LayerToName(this.gameObject.layer + 1));
                ArtilleryTurretShell artilleryTurretShell = spawned.GetComponent<ArtilleryTurretShell>();
                artilleryTurretShell.layermask = transform.parent.parent.GetComponent<TargetingSystem>().layermask;
                artilleryTurretShell.tag = this.gameObject.transform.parent.tag;
            }
        }
    }
}
