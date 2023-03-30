using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShadowInRad : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;

    private Collider2D[] asteroidInRadList;
    void Start()
    {
        asteroidInRadList = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach(Collider2D collider in asteroidInRadList){
            if(collider.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>()){
                collider.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().enabled = true;
            }
        }
    }
}
