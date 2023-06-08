using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingBeams : MonoBehaviour
{
    private Vector3[] cornerPos = new Vector3[2];
    [SerializeField] List<Transform> weldPos;
    [SerializeField] private List<Transform> weldEmitters;
    [SerializeField] private List<LineRenderer> lineRenderers;
    private float[] timeElapsed = new float[2];
    [HideInInspector] public GameObject building;
    private BoxCollider2D b = null;
    [SerializeField] private List<UnityEngine.VFX.VisualEffect> effects;


    // Start is called before the first frame update
    void Start()
    {
        foreach(UnityEngine.VFX.VisualEffect effect in effects){
            effect.Stop();
        }
        foreach(LineRenderer lineRenderer in lineRenderers){
            lineRenderer.positionCount = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(b == null && building != null){
            b = building.GetComponent<BoxCollider2D>();
            cornerPos[0] = building.transform.TransformPoint(b.offset + new Vector2(b.size.x, -b.size.y)*0.5f);
            cornerPos[1] = building.transform.TransformPoint(b.offset + new Vector2(-b.size.x, -b.size.y)*0.5f);
            weldPos[0].position = cornerPos[0];
            weldPos[1].position = cornerPos[1];
            effects[0].Play();
            effects[1].Play();
        }

        if(building != null){
            //Debug.Log("Building Set");
            if(weldPos[0].position == cornerPos[0] || weldPos[0].position == cornerPos[1]){
                foreach(Transform weldPosition in weldPos){
                    if(weldPosition.position == cornerPos[0]){
                        StartCoroutine(ShiftLeft(weldPosition));
                    }
                    if(weldPosition.position == cornerPos[1]){
                        StartCoroutine(ShiftRight(weldPosition));
                    }
                }
            }
            
            
            lineRenderers[0].SetPosition(0, weldEmitters[0].position);
            lineRenderers[0].SetPosition(1, weldPos[0].position);
            lineRenderers[1].SetPosition(0, weldEmitters[1].position);
            lineRenderers[1].SetPosition(1, weldPos[1].position);
            if(lineRenderers[0].enabled == false){
                lineRenderers[0].enabled = true;
                lineRenderers[1].enabled = true;
            }
        }
        else{
            b = null;
            lineRenderers[0].enabled = false;
            lineRenderers[1].enabled = false;
            effects[0].Reinit();
            effects[1].Reinit();
            effects[0].Stop();
            effects[1].Stop();
        }
    }

    private IEnumerator ShiftLeft(Transform weldPosition){
        timeElapsed[1] += Time.deltaTime * 3;
        if(building == null){
            yield break;
        }
        while(weldPosition.position != cornerPos[1] && building != null){
            weldPosition.position = Vector3.Lerp(weldPosition.position, cornerPos[1], timeElapsed[1]);
            if(Vector2.Distance(weldPosition.position, cornerPos[1]) < 1f){
                weldPosition.position = cornerPos[1];
                timeElapsed[1] = 0;
                yield break;
            }
            yield return null;
        }    

        timeElapsed[1] = 0;
        yield break;
    }
    private IEnumerator ShiftRight(Transform weldPosition){
        timeElapsed[0] += Time.deltaTime * 3;
        if(building == null){
            yield break;
        }
        while(weldPosition.position != cornerPos[0] && building != null){
            weldPosition.position = Vector2.Lerp(weldPosition.position, cornerPos[0], timeElapsed[0]);
            if(Vector2.Distance(weldPosition.position, cornerPos[0]) < 1f){
                weldPosition.position = cornerPos[0];
                timeElapsed[0] = 0;
                yield break;
            }
            yield return null;
        }    

        timeElapsed[0] = 0;
        yield break;
    }
}
