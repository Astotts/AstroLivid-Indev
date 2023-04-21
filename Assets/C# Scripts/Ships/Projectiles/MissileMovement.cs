using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject particles;

    public float speed = 45f;
    public float rotateSpeed = 4000f;

    public GameObject explosionEffect;

    public Rigidbody2D rb;

    private float lifeDuration = 8f;
    private float lifeElapsed = 0f;

    private float deployElapsed = 0f;
    private float deployDuration = 2f;

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
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        if(target != null){
            deployElapsed -= Time.deltaTime;
            if(deployElapsed <= 0){
                Vector3 look = transform.InverseTransformPoint(target.transform.position);
                float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            
                transform.Rotate(0, 0, angle);

                rb.velocity = (Vector2)transform.up * speed;
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
            }
            else{
                Vector2 direction = (Vector2)target.position - rb.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;

                rb.angularVelocity = -rotateAmount * rotateSpeed;
                rb.velocity += (Vector2)transform.up * speed;
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D){
        //Debug.Log("Missile Collided");
        if(collider2D.tag != gameObject.tag){
            Death();
        }
    }
    
    void Death(){
        colliderArray.Clear();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        colliderArray.AddRange(Physics2D.OverlapCircleAll(this.transform.position, 5, layermask));
        foreach(Collider2D collider in colliderArray){
            collider.GetComponent<HealthManager>().LowerHealth(damage);
        }
        Destroy(gameObject);
    }
}
