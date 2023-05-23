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
    public BuildingIdentifyer building;
    [SerializeField] public Transform home;
    [SerializeField] public Transform anchorPoint;

    private float angle = 0;
    private float radius = 30f;

    void Start(){
        destination = home;
    }
    

    void FixedUpdate(){
        direction = this.transform.position - destination.position;
        direction = direction.normalized;
        rotateAmount = Vector3.Cross(direction, transform.up).z;
        unitRB.angularVelocity = rotateAmount * rotateSpeed;
        distance = Vector2.Distance(transform.position, destination.position);
        if(destination != home && distance < 30f){
            //Debug.Log("Test1");
            if(angle == 0){
                angle = Vector3.Angle(this.transform.position - destination.position, destination.up);
                unitRB.velocity = Vector2.zero;
                Debug.Log(angle);
            }
            
            if(transform.rotation.z < destination.rotation.z + 0.1f && transform.rotation.z > destination.rotation.z - 0.1f){
                //RotateAround(1f);
                unitRB.velocity += (Vector2)transform.up * moveSpeed / 2 * Time.deltaTime;
                if(Vector3.Distance(anchorPoint.position, destination.position) < 3f){
                //Debug.Log("Test4");
                    atDestination = true;
                    if(destination != home){
                        this.PlacePiece();
                        angle = 0;
                    }
                }
                else{
                    //Debug.Log("Test5");
                    atDestination = false;
                }
            }
            else{
                unitRB.velocity = Vector2.zero;
                RotateAround(35f);
            }
        }
        else if(distance < moveSpeed + 10f){
            //Debug.Log("Test2");
            unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime; 
            unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, ((moveSpeed / (distance + moveSpeed) + distance - 1)));
        }
        else{
            //Debug.Log("Test3");
            unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime;   
            unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, moveSpeed);
        }
    }

    public void SetPiece(GameObject piece_, BuildingIdentifyer building_){
        //Debug.Log("Instatiated Piece");
        this.piece = Instantiate(piece_, this.anchorPoint.position, new Quaternion(0f,0f,0f,0f), this.anchorPoint);
        anchorPoint.position = transform.position + (transform.up * (this.piece.GetComponent<Collider2D>().bounds.extents.y + this.shipCollider.bounds.extents.y));
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
        building.PlacePart();
        //Call a method on the structure to update health and eventually enable the structure
    }

    private void RotateAround(float deg){
        unitRB.velocity = Vector2.zero;
        float x = 0;
        float y = 0;
        if(Vector3.Distance(transform.right + transform.position, destination.position -destination.up * 30f) > Vector3.Distance(-transform.right + transform.position, destination.position -destination.up * 30f)){
            angle -= deg * Mathf.Deg2Rad * Time.deltaTime;
        }
        else{
            angle += deg * Mathf.Deg2Rad * Time.deltaTime;
        }
        
        x = radius * Mathf.Cos(angle) + destination.position.x;
        y = radius * Mathf.Sin(angle) + destination.position.y;

        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0f), Time.deltaTime);
    }
}
