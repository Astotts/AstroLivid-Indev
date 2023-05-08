using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIdentifyer : UnitIdentifyer
{
    [SerializeField] float moveSpeed;
    [SerializeField] float defaultMoveSpeed;
    float rotateAmount;
    float rotateSpeed = 400f;
    
    public Collider2D colliderAttack;

    private float healthResetTimer = 0.3f;
    private float healthTimer;
    public bool built;

    public Vector2 direction;
    private float distance;

    private void Start(){
        destination = transform.position;
        moveSpeed = defaultMoveSpeed;
        //this.gameObject.tag = this.gameObject.transform.parent.tag;
        healthManager = gameObject.GetComponent<HealthManager>();
        SetSelectedVisible(false);
        SetHealthVisible(false);
    }

    void FixedUpdate(){
        direction = this.transform.position - destination;
        direction = direction.normalized;
        rotateAmount = Vector3.Cross(direction, transform.up).z;
        unitRB.angularVelocity = rotateAmount * rotateSpeed;

        if(!atDestination){
            distance = Vector2.Distance(transform.position, destination);
            if(distance < moveSpeed){
                unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime; 
                unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, ((moveSpeed / (distance + moveSpeed) + distance - 1)));
                if(distance < moveSpeed / 20f){
                    unitRB.velocity = Vector2.zero;
                    atDestination = true;
                }
            }
            else{
                unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime;   
                unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, moveSpeed);
            }    
        }
        

        if(healthManager.maxHealth > healthManager.health){
            healthTimer += 1f * Time.deltaTime;
        }
        else{
            built = true;
        }
        
        if(!built && healthTimer >= healthResetTimer){
            healthManager.health += 1;
            healthTimer = 0f;
        }
    }
}
