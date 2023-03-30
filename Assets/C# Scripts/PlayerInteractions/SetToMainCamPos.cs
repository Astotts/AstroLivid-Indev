using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToMainCamPos : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private Transform parent;
    private CameraMovement parentCamMovement;
    private Camera parentCam;
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        parentCamMovement = parent.GetComponent<CameraMovement>();
        parentCam = parent.GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = transform.parent.position;
        cam.orthographicSize = parentCam.orthographicSize;
    }
}
