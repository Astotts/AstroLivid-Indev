using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    // State
    [HideInInspector]
    public Vector2 position;
    [HideInInspector]
    public Vector2 forward;
    Vector2 velocity;
    [SerializeField]
    public FlowField curFlowField;

    // To update:
    Vector2 acceleration;
    [HideInInspector]
    public Vector2 avgFlockHeading;
    [HideInInspector]
    public Vector2 avgAvoidanceHeading;
    [HideInInspector]
    public Vector2 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;
    [SerializeField]
    private float targetWeight;
    [SerializeField]
    private float alignWeight;
    [SerializeField]
    private float cohesionWeight;
    [SerializeField]
    private float seperateWeight;
    [SerializeField]
    private float avoidCollisionWeight;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float boundsRadius;
    [SerializeField]
    private float collisionAvoidDst;
    [SerializeField]
    private LayerMask obstacleMask;
    [SerializeField]
    private float maxSteerForce;

    //Steering
    Vector3 moveDir;
    private float rotateAmount;
    public Rigidbody2D rb;
    public float rotateSpeed = 400f;

    Vector3 cellDirection;

    Transform target;

    void Awake(){
        forward = transform.forward;
    }

    public void Update () {
        if (curFlowField == null) { return; }
        Vector2 acceleration = Vector2.zero;

        Cell cellBelow = curFlowField.GetCellFromWorldPos(this.transform.position);
        Vector2 moveDirection = new Vector2(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y);
        acceleration = SteerTowards (moveDirection) * targetWeight;

    

        /*if (target != null) {
            Vector2 offsetToTarget = (target.position - position);
            acceleration = SteerTowards (offsetToTarget) * settings.targetWeight;
        }*/

        //This could be used to fire at enemies directly

        //Debug.Log(curFlowField);
        
        //Debug.Log(acceleration);
        /*To Do
        Research steering behaviors and smooth movement for AI;
        */

        
        //Going to have to write my own code to pass along a list of all the boids in a group
        /*if (numPerceivedFlockmates != 0) {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector2 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards (avgFlockHeading) * alignWeight;
            var cohesionForce = SteerTowards (offsetToFlockmatesCentre) * cohesionWeight;
            var seperationForce = SteerTowards (avgAvoidanceHeading) * seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }*/

        if (IsHeadingForCollision ()) {
            Vector2 collisionAvoidDir = ObstacleRays();
            Vector2 collisionAvoidForce = SteerTowards (collisionAvoidDir) * avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        //Debug.Log(velocity);
        float speed = velocity.magnitude;
        Vector2 dir = velocity / speed;
        //Debug.Log(dir);
        speed = Mathf.Clamp (speed, minSpeed, maxSpeed);
        velocity = dir * speed;
        //Debug.Log(velocity);

        transform.position += (Vector3)velocity * Time.deltaTime;
        forward = dir;
    }

    bool IsHeadingForCollision () {
        if (Physics2D.OverlapCircle (transform.position, collisionAvoidDst, obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    Vector2 ObstacleRays () {
        Collider2D hit;
        Vector2 hitDirection;
        hit = Physics2D.OverlapCircle (transform.position, collisionAvoidDst, obstacleMask);
        hitDirection = (Vector2)(transform.position - hit.transform.position);
        hitDirection = hitDirection.normalized;
        return hitDirection;
    }

    Vector2 SteerTowards (Vector2 vector) {
        Vector2 v = vector.normalized * maxSpeed - velocity;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, v);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 200f * Time.deltaTime);
        return Vector2.ClampMagnitude (v, maxSteerForce);
    }

    /*private void OnDrawGizmos(){
        //Physics2D.CircleCast (transform.position, boundsRadius, forward, collisionAvoidDst, obstacleMask)
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, boundsRadius, 0.5f);
    }*/

}