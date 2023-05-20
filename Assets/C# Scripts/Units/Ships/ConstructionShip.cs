using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionShip : MonoBehaviour
{
    [SerializeField] private Collider2D shipCollider;

    [SerializeField] private Rigidbody2D unitRB;
    
    [SerializeField] float moveSpeed;
    [SerializeField] float defaultMoveSpeed;
    float rotateAmount;
    float rotateSpeed = 400f;

    public Vector2 direction;
    private float distance;

    public ConstructionStates constructionState;

    private bool atDestination = true;
    public Transform destination;
    [SerializeField] public Transform home;

    void Start(){
        destination = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, destination.position) < 25f){
            atDestination = true;
            //Figure out how to make it a range of degrees
            if(transform.rotation != destination.rotation){
                transform.RotateAround(destination.position, new Vector3(0f,0f,destination.position.z), 90f * Time.deltaTime);
            }
            //BuildingIdentifyer.PlacePiece;
            //constructionState = ConstructionStates.Done;
            //destination = home;
        }
    }

    void FixedUpdate(){
        direction = this.transform.position - destination.position;
        direction = direction.normalized;
        rotateAmount = Vector3.Cross(direction, transform.up).z;
        unitRB.angularVelocity = rotateAmount * rotateSpeed;

        if(!atDestination){
            distance = Vector2.Distance(transform.position, destination.position);
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
    }

    public void SetPiece(GameObject piece){
        Instantiate(piece, Vector3.zero, new Quaternion(0f,0f,0f,0f), this.transform);
        //collider.ClosestPoint(this.transform.position);
    }
}
