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
    public bool carrying = false;
    public Transform destination;
    public GameObject piece;
    private BuildingIdentifyer building;
    [SerializeField] public Transform home;

    void Start(){
        destination = home;
    }

    void FixedUpdate(){
        direction = this.transform.position - destination.position;
        direction = direction.normalized;
        rotateAmount = Vector3.Cross(direction, transform.up).z;
        unitRB.angularVelocity = rotateAmount * rotateSpeed;

        distance = Vector2.Distance(transform.position, destination.position);
        if(destination != home && distance > 15f && distance < 10f){
            Debug.Log("Test1");
            unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, 0f);
            transform.RotateAround(destination.position, new Vector3(0f,0f,destination.position.z), 90f * Time.deltaTime);
            if(transform.rotation.z + 5f > destination.rotation.z || transform.rotation.z - 5f > destination.rotation.z){
                unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime;
            }
        }
        else if(distance < moveSpeed + 10f){
            Debug.Log("Test2");
            unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime; 
            unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, ((moveSpeed / (distance + moveSpeed) + distance - 1)));
        }
        else{
            Debug.Log("Test3");
            unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime;   
            unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, moveSpeed);
        }
        if(distance < 10f){
            Debug.Log("Test4");
            atDestination = true;
            if(destination != home){
                this.PlacePiece();
            }
        }
        else{
            Debug.Log("Test5");
            atDestination = false;
        }
    }

    public void SetPiece(GameObject piece_, BuildingIdentifyer building_){
        Debug.Log("Instatiated Piece");
        this.piece = Instantiate(piece_, this.transform.position, new Quaternion(0f,0f,0f,0f), this.transform);
        carrying = true;
        this.building = building_; 
        //collider.ClosestPoint(this.transform.position);
    }

    private void PlacePiece(){
        constructionState = ConstructionStates.Done;
        carrying = false;
        piece.transform.position = destination.position;
        piece.transform.rotation = destination.rotation;
        piece.transform.parent = destination;
        destination = home;
        //Call a method on the structure to update health and eventually enable the structure
    }
}
