using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidIdentifyer : MonoBehaviour
{
    public static AsteroidIdentifyer instance;
    public /*FlowFieldNS*/FlowField curFlowField;
    public GameObject gridController;
    private GridController gridControllerScript;

    private Vector2 spawnPosition;

    private Rigidbody2D asteroidRB; 
    public bool grabbed;
    public bool selected;

    public float defaultMoveSpeed;
    private float doubledMoveSpeed;
    public float moveSpeed;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teamHandler;
    
    private TeamHandler teamHandlerScript;

    private HealthManager healthManager;

    private void Start() {
        spawnPosition = gameObject.transform.position;
        instance = this;
        healthManager = GetComponent<HealthManager>();
        gridControllerScript = gridController.GetComponent<GridController>();   
        asteroidRB = GetComponent<Rigidbody2D>();
        //ramming = !ramming;
        doubledMoveSpeed = defaultMoveSpeed * 2f;
        moveSpeed = defaultMoveSpeed;
        //this.curFlowField = gridControllerScript.Move(gameObject.transform.position);
        if(gameObject.layer == 6){
            teamHandlerScript.SetUpInstatiatedObject(gameObject, true);
        }
    }

    /*void FixedUpdate(){
        if(grabbed){
            curFlowField = null;
        }
    }*/

    public void SetFlowField(/*FlowFieldNS*/FlowField curFlowField){
        this.curFlowField = curFlowField;
    }

    /*public void CreateFlowField(Vector3 position){
        this.curFlowField = gridControllerScript.Move(position);
        //Debug.Log(curFlowField);
    }*/
    
}

