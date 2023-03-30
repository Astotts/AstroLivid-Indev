using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMiningBeam : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private RaycastHit2D hit;

    [SerializeField] private GameObject parentShip;
    [SerializeField] private LayerMask layerMask;
    public Transform p0;
    public Vector3 p1;
    public Transform p2;

    private Vector2 rayCheck;

    private MiningFighterWeaponsHandler targetingScript;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        p0 = gameObject.transform;
        gameObject.tag = parentShip.tag;
        targetingScript = parentShip.GetComponent<MiningFighterWeaponsHandler>();
    }

    void Update()
    {
        if(p0 && p2){
            rayCheck = p0.position - p2.position;
            hit = Physics2D.Raycast(transform.position, rayCheck.normalized, -rayCheck.magnitude, layerMask);
            
            //Debug.Log(hit.collider);
            p1 = (transform.position + p2.position) / 2f;
            Debug.Log(p0.position);
            Debug.Log(p1);
            Debug.Log(targetingScript.asteroid.transform.position);
            if(hit && targetingScript.asteroidTargeted && targetingScript.asteroid == targetingScript.savedAsteroid){
                lineRenderer.enabled = true;
                DrawQuadraticBezierCurve(p0.position, p1, targetingScript.asteroid.transform.position);
            }
            else{
                lineRenderer.enabled = false;
            }
        }
    }

    void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
    {
        lineRenderer.positionCount = 200;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
        }
    }
}
