using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    [SerializeField] float panSpeed = 20f;
    public float panBorderThickness = 1f;
    [SerializeField] Vector3 panLimit;
    
    private float mouseWheelLerpIncrement;
    [SerializeField] Camera cam;
    private Vector3 dragOrigin;

    private float positionLerp;

    [SerializeField] float zoomOutMin = 5f;
    [SerializeField] float zoomOutMax = 250f;
    [SerializeField] float scrollSpeed;
    [SerializeField] float baseScrollSpeed = 10f;

    /*private Vector2 mouseOnWorldPos1;
    private Vector2 mouseOnWorldPos0;*/
    private Vector3 pos;

    void Update()
    {
        if(!Input.GetMouseButtonDown(2)){
        
        pos = transform.position;
        
        if((Input.GetAxis("Vertical") > 0) || (Input.mousePosition.y >= Screen.height - panBorderThickness)){
            pos.y += panSpeed * Time.deltaTime * Camera.main.orthographicSize/5;
        }
        if((Input.GetAxis("Vertical") < 0) || (Input.mousePosition.y <= panBorderThickness)){
                pos.y -= panSpeed * Time.deltaTime * Camera.main.orthographicSize/5;
        }
        if((Input.GetAxis("Horizontal") > 0) || (Input.mousePosition.x >= Screen.width - panBorderThickness)){
            pos.x += panSpeed * Time.deltaTime * Camera.main.orthographicSize/5;
        }
        if((Input.GetAxis("Horizontal") < 0) || (Input.mousePosition.x <= panBorderThickness)){
            pos.x -= panSpeed * Time.deltaTime * Camera.main.orthographicSize/5;
        }
        /** ^ gets position in 3D space of the camera and if the mouse cursor comes close to the sides of the screen outlined by (panBorderThickness) then it will
        add or subtract depending on the direction panSpeed * Time.deltaTime (delta time standing for difference in time passed in seconds)
        **/
        scrollSpeed = baseScrollSpeed/10f * Camera.main.orthographicSize;
        mouseWheelLerpIncrement = Mathf.Lerp(mouseWheelLerpIncrement, Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, 10f * Time.deltaTime);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - mouseWheelLerpIncrement, zoomOutMin, zoomOutMax);
        pos.z = -5 + -Camera.main.orthographicSize;
        positionLerp = Mathf.Lerp(positionLerp, Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, 10f * Time.deltaTime);

        /*if(Input.GetAxis("Mouse ScrollWheel") != 0){
            mouseOnWorldPos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.x += (mouseOnWorldPos0.x - mouseOnWorldPos1.x);
            pos.y += (mouseOnWorldPos0.y - mouseOnWorldPos1.y);
        }
        else{
            mouseOnWorldPos0 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }*/
        
        //Zoom function which sets scroll to the value of the mouse scrollwheel and adds it to the Z coordinate (camera needs to be in perspective NOT orthographic) 
        pos.x = Mathf.Clamp(pos.x, 20f, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, 20f, panLimit.y);    
        pos.z = Mathf.Clamp(pos.z, 0f, 0f);   
        
        transform.position = pos;
        }
        
        if(Input.GetMouseButtonDown(2))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(2)){
        Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
        cam.transform.position += difference;
        }
    }
}