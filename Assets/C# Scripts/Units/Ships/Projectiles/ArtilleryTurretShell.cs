using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTurretShell : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private float artilleryTime = 3f;
    private float currentArtilleryTime;

    private float speed = 75f;

    private List<Collider2D> colliderArray;
    public LayerMask layermask;
    private float damage = 5;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        colliderArray = new List<Collider2D>();
        currentArtilleryTime = artilleryTime;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        currentArtilleryTime -= 1 * Time.deltaTime;

        if(currentArtilleryTime <= 0f){
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        //colliderArray.Clear();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        HealthManager enemyHealth = collider2D.GetComponent<HealthManager>();
        if(enemyHealth != null){ enemyHealth.LowerHealth(damage); }
        Destroy(gameObject);
    }
    
    /*void Death(){
        colliderArray.Clear();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //colliderArray.AddRange(Physics2D.OverlapCircleAll(this.transform.position, 5, layermask));
        collider2D.GetComponent<HealthManager>().LowerHealth(damage);
        /*foreach(Collider2D collider in colliderArray){
            Debug.Log("Test");
            collider.GetComponent<HealthManager>().LowerHealth(damage);
        }
        Destroy(gameObject);
    }*/
}
