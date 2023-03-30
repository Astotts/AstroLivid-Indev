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
    
    public Collider2D colliderAttack;

    public bool selected;

    private HealthManager healthManager;

    private float healthResetTimer = 0.3f;
    private float healthTimer;
    public bool built;

    //public Vector3 destination;

    private void Awake(){
        moveSpeed = defaultMoveSpeed;
        //this.gameObject.tag = this.gameObject.transform.parent.tag;
        healthManager = gameObject.GetComponent<HealthManager>();
        SetSelectedVisible(false);
        SetHealthVisible(false);
    }

    void FixedUpdate(){
        //Debug.Log(curFlowField);
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
            collider2DArray = Physics2D.OverlapAreaAll(UtilsClass.GetMouseWorldPosition(), UtilsClass.GetMouseWorldPosition());
            foreach(Collider2D collider2D in collider2DArray){
                if(collider2D.GetComponent<UnitIdentifyer>()){
                    collider2D.GetComponent<UnitIdentifyer>().SetHealthVisible(true);
                }
            }
            hovering = true;
        }
        //If your mouse hovers over the GameObject with the script attached, output this message
    }

    void OnMouseExit()
    {
        foreach(Collider2D collider2D in collider2DArray){
            if(collider2D.GetComponent<UnitIdentifyer>() && collider2D.GetComponent<UnitIdentifyer>().GetSelectedVisible() == false){
                collider2D.GetComponent<UnitIdentifyer>().SetHealthVisible(false);    
            }
        }
        hovering = false;
        //The mouse is no longer hovering over the GameObject so output this message each frame
    }
}
