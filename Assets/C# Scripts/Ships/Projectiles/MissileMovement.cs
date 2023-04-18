using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject particles;

    public float speed = 25f;
    public float rotateSpeed = 200f;

    public GameObject explosionEffect;

    private Rigidbody2D rb;

    private float missileTime = 5f;
    private float currentMissileTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentMissileTime = missileTime;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMissileTarget(Collider2D enemy){
        target = enemy.transform;
    }
    //gets the enemies position from ArtilleryFighterWeapons
    
    void Update() {
        currentMissileTime -= 1 * Time.deltaTime;
        if(currentMissileTime <= 0 && target != null){
            target = null;
            rb.angularVelocity = Random.Range(-0.7f, 0.7f) * rotateSpeed;
            //particles.SetActive(false);
        }
        if(currentMissileTime <= -2){
            currentMissileTime = missileTime;
            Destroy(gameObject);
        }
    }
    //Once the missile time is 0 it is reset and the missile is destroyed as to allow for smaller ships to escape missiles

    // Update is called once per frame
    void FixedUpdate() {
        //Debug.Log(target);
        if(target != null){
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity += (Vector2)transform.up * speed;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        }
        /*Cross product (using 2 3D vectors to get a vector that is orthogonal (pointing inwards or outwards from the screen) to the other two vectors) 
        in other words you get a magnitude and a direction. Uses the target's position and the missiles position to get the direction by turning it into a Vector.
        It then uses the cross product to find its rotate amount and rotates it by setting the rigidbody's angularVelocity to the cross product times the rotate speed
        it sets the missile to transform towards the missile's localized "up" multiplied by its speed*/
    }
    void OnTriggerEnter2D(Collider2D collider2D){
        //Debug.Log("Missile Collided");
        if(collider2D.tag != gameObject.tag){
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    //Spawns in an explosion effect on the missile position then destroys the missile
}
