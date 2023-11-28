using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMissile : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject particles;

    private float speed = 45f;
    public float rotateSpeed = 4000f;

    public GameObject explosionEffect;

    public Rigidbody2D rb;

    private float lifeDuration = 3f;
    private float lifeElapsed = 0f;

    private float deployElapsed = 0f;
    private float deployDuration = 3f;

    private List<Collider2D> colliderArray;
    public LayerMask layermask;
    private float damage = 5;

    void Awake()
    {
        colliderArray = new List<Collider2D>();
        lifeElapsed = lifeDuration;
        deployElapsed = deployDuration;
        rb.AddForce((Vector2)transform.up * speed);
    }

    public void SetMissileTarget(Collider2D enemy){
        target = enemy.transform;
    }
    
    void Update() {
        lifeDuration -= Time.deltaTime;
        if(lifeDuration <= 0 && target != null){
            target = null;
            rb.angularVelocity = Random.Range(-0.7f, 0.7f) * rotateSpeed;
            //particles.SetActive(false);
        }
        if(lifeDuration <= -2){
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        rb.velocity = (Vector2)transform.up * speed;
        if(target != null){
            deployElapsed -= Time.deltaTime;
            rb.velocity = (Vector2)transform.up * speed;
            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D){
        Instantiate(explosionEffect, transform.position, transform.rotation);
        HealthManager enemyHealth = collider2D.GetComponent<HealthManager>();
        if(enemyHealth != null){ enemyHealth.LowerHealth(damage); }
        Destroy(gameObject);
    }
    
    /*void Death(){
        colliderArray.Clear();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        colliderArray.AddRange(Physics2D.OverlapCircleAll(this.transform.position, 5, layermask));
        foreach(Collider2D collider in colliderArray){
            collider.GetComponent<HealthManager>().LowerHealth(damage);
        }
        Destroy(gameObject);
    }*/
}
