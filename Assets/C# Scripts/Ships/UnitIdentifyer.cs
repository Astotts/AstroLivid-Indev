using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class UnitIdentifyer : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameObject;

    [SerializeField] private GameObject selectedHealthObject;

    [SerializeField] private Rigidbody2D unitRB;
    
    [SerializeField] float moveSpeed;
    [SerializeField] float defaultMoveSpeed;
    float rotateAmount;
    float rotateSpeed = 400f;
    
    public Collider2D colliderAttack;

    public bool selected;

    private HealthManager healthManager;

    private float healthResetTimer = 0.3f;
    private float healthTimer;
    public bool built;

    public Vector2 direction;
    public Vector3 destination;
    private float distance;
    private bool atDestination = true;

    public UnitType.UnitVariant variant;

    private void Start(){
        destination = transform.position;
        moveSpeed = defaultMoveSpeed;
        //this.gameObject.tag = this.gameObject.transform.parent.tag;
        healthManager = gameObject.GetComponent<HealthManager>();
        SetSelectedVisible(false);
        SetHealthVisible(false);
    }

    public void MoveTo(Vector2 pos){
        destination = pos;
        atDestination = false;
        Debug.Log(destination);
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

    //Methods below are nessesary for specific cases
    public void SetSelectedVisible(bool visible){
        if(selectedGameObject != null){
        selectedGameObject.SetActive(visible);
        }
    }
    public void SetHealthVisible(bool visible){
        if(selectedHealthObject != null){
        selectedHealthObject.SetActive(visible);
        }
        
    }
    
    public bool GetSelectedVisible(){
        return selectedGameObject.activeInHierarchy;
    }

        private Collider2D[] collider2DArray;

    private bool hovering = false;

    void OnMouseOver()
    {
        if(!hovering){
            selectedHealthObject.SetActive(true);
            selectedGameObject.SetActive(true);
        }
        hovering = true;
    }

    void OnMouseExit()
    {
        if(!selected){
            selectedGameObject.SetActive(false);   
        }
        selectedHealthObject.SetActive(false);
        hovering = false;
    }
}
