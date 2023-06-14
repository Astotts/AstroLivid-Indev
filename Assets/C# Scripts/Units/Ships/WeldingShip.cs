using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingShip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D unitRB;
    [SerializeField] private Animator animator;
    
    [SerializeField] float moveSpeed;
    [SerializeField] float defaultMoveSpeed;
    float rotateAmount;
    float rotateSpeed = 400f;

    public Vector2 direction;
    private float distance;

    public ConstructionStates constructionState;

    public Transform assignedDestination;
    public Transform destination;
    public BuildingIdentifyer building;
    [SerializeField] public Transform home;
    [SerializeField] private Transform entry;
    [SerializeField] private WeldingBeams weldingBeams;

    private BoxCollider2D b;

    private float angle = 0;
    private float radius = 40f;

    [SerializeField] private List<SpriteRenderer> attachedSpriteList;

    void Start(){
        destination = home;
    }



    void FixedUpdate(){
        if(attachedSpriteList[0].sortingLayerName != "Default"){
            if(destination == entry && Vector2.Distance(transform.position, entry.position) < 3f){
                destination = home;
                foreach(SpriteRenderer spriteRenderer in attachedSpriteList){
                    spriteRenderer.sortingLayerName = "Default";
                }
            }
        }
        else{
            if(destination == entry && Vector2.Distance(transform.position, entry.position) < 20f){
                destination = assignedDestination;
                foreach(SpriteRenderer spriteRenderer in attachedSpriteList){
                    spriteRenderer.sortingLayerName = "Above";
                }
            }
        }
        direction = this.transform.position - destination.position;
        direction = direction.normalized;
        rotateAmount = Vector3.Cross(direction, transform.up).z;
        unitRB.angularVelocity = rotateAmount * rotateSpeed;
        distance = Vector2.Distance(transform.position, destination.position);
        if(destination != home && destination != entry && distance < radius){
            if(angle == 0){
                angle = (transform.eulerAngles.z - 90) / 180 * Mathf.PI;
                unitRB.velocity = Vector2.zero;
            }

            if(transform.rotation.z < destination.rotation.z + 0.1f && transform.rotation.z > destination.rotation.z - 0.1f){
                if(building.built){
                    animator.SetTrigger("Deploy");
                }
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Active")){
                    if(weldingBeams.building == null){
                        weldingBeams.building = this.building.gameObject;    
                    }
                    if(building.totalWeldTime > building.weldTime){
                        building.weldTime += Time.deltaTime;
                    }
                    else{
                        weldingBeams.building = null;
                        animator.SetTrigger("Withdraw");
                        animator.ResetTrigger("Deploy");
                        WeldPiece();
                        angle = 0;

                    }
                    building.UpdateWeld();
                }
            }
            else{
                unitRB.velocity = Vector2.zero;
                RotateAround(radius);
            }
        }
        else if(distance < moveSpeed + 30f){
            //Debug.Log("Test2");
            if(destination != home && destination != entry){
                unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime; 
                unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, ((moveSpeed / (30 + moveSpeed) + distance - 20)));
            }
            else{
                unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime; 
                unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, ((moveSpeed / (30 + moveSpeed) + distance - 1)));
            }
        }
        else{
            unitRB.velocity += (Vector2)transform.up * moveSpeed * Time.deltaTime;   
            unitRB.velocity = Vector2.ClampMagnitude(unitRB.velocity, moveSpeed);
        }
    }

    public void SetWeld(){
        destination = entry;
        transform.position = home.position;
        transform.rotation = home.rotation;
    }

    private void WeldPiece(){
        constructionState = ConstructionStates.Done;
        destination = entry;
        assignedDestination = null;
        building.Weld();
        foreach(SpriteRenderer spriteRenderer in attachedSpriteList){
            spriteRenderer.sortingLayerName = "Above";
        }
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

