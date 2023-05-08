using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSteering : MonoBehaviour
{
    private Vector3 velocityVector;
    private Vector2 direction;
    private float rotateAmount;
    public Rigidbody2D rb;
    public float rotateSpeed = 400f;

    Vector3 cellDirection;

    public Collider2D asteroid;

    void Update()
    {
        Vector3 movePosition = gameObject.transform.position + cellDirection;
        Vector3 moveDir = (transform.position - movePosition).normalized;
        velocityVector = moveDir;
    }

    void FixedUpdate(){

        if(!asteroid){
            direction = velocityVector;
            direction.Normalize();
            rotateAmount = Vector3.Cross(direction, transform.up).z;
        }
        else if(asteroid){
            direction = (Vector2)asteroid.transform.position - rb.position;
            direction.Normalize();
            rotateAmount = Vector3.Cross(-direction, transform.up).z;
        }

        

        rb.angularVelocity = rotateAmount * rotateSpeed;
    }

    public void SetDirection(Vector3 _cellDirection){
        cellDirection = _cellDirection;
    }
}