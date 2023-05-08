using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdentifyer : MonoBehaviour
{

    [SerializeField] protected GameObject selectedGameObject;

    [SerializeField] protected GameObject selectedHealthObject;

    [SerializeField] protected Rigidbody2D unitRB;

    protected HealthManager healthManager;

    public bool selected;

    public UnitType.UnitVariant variant;

    protected bool atDestination = true;
    public Vector3 destination;

    public void MoveTo(Vector2 pos){
        destination = pos;
        atDestination = false;
        Debug.Log(destination);
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
