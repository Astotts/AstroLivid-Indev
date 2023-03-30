using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    public int maxHealth;
    public int health;

    [SerializeField] private HealthGUI healthGUIManager;

    void Start()
    {
        //if(!gameObject.GetComponent<UnitIdentifyer>() && !gameObject.GetComponent<BuildingIdentifyer>()){
            health = maxHealth;
        //}

    }  

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag(this.tag)){
            if(collision.gameObject.GetComponent<HealthManager>() && collision.gameObject.tag != tag){
                int damage = collision.gameObject.GetComponent<HealthManager>().health;
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
    }    
}


