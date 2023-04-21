using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTurretShell : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private float artilleryTime = 2f;
    private float currentArtilleryTime;

    private float speed = 70f;

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
        if(collider2D.tag != gameObject.tag){
            Death();
        }
    }
    
    void Death(){
        colliderArray.Clear();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        colliderArray.AddRange(Physics2D.OverlapCircleAll(this.transform.position, 5, layermask));
        foreach(Collider2D collider in colliderArray){
            collider.GetComponent<HealthManager>().health -= damage;
        }
        Destroy(gameObject);
    }
}
