using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //[SerializeField] private GameObject explosionEffect;
    public float maxHealth;
    public float health;

    [SerializeField] private HealthGUI healthGUIManager;

    void Awake()
    {
        //if(!gameObject.GetComponent<UnitIdentifyer>() && !gameObject.GetComponent<BuildingIdentifyer>()){
            health = maxHealth;
        //}

    }  

    /*void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag(this.tag)){
            if(collision.gameObject.GetComponent<HealthManager>() && collision.gameObject.tag != tag){
                float damage = collision.gameObject.GetComponent<HealthManager>().health;
                if(health <= damage){
                    health -= damage;
                    Instantiate(explosionEffect, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else{
                    health -= damage;
                }
            }
            healthGUIManager.UpdateHealth(health, maxHealth);
        }
    }*/

    public void LowerHealth(float damage){
        health -= damage;
        if(health <= damage){
            //Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        healthGUIManager.UpdateHealth(health, maxHealth);
    }
}


