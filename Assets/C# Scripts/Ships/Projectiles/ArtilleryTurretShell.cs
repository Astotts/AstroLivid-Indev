using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTurretShell : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private float artilleryTime = 2f;
    private float currentArtilleryTime;

    private float speed = 70f;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        currentArtilleryTime = artilleryTime;
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Debug.Log("Spawned");
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
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
}
