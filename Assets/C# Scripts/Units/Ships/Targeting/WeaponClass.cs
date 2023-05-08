using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    protected Collider2D target;

    public void SetTarget(Collider2D enemy){
        this.target = enemy;
    }
}
