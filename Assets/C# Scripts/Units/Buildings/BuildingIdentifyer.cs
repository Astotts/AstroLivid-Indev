using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIdentifyer : UnitIdentifyer
{
    public float creditCost;
    public float populationCost;
    public float powerCost;
    
    //Construction
    [HideInInspector] public bool built = false;
    [SerializeField] public float totalWeldTime;
    [HideInInspector] public float weldTime = 0;
    [HideInInspector] private float weldAlpha;
    [HideInInspector] public BuildOrder buildOrder;
    [SerializeField] private List<GameObject> piecesList;
    [SerializeField] private List<Transform> positionList;
    [SerializeField] private int partCount;
    private int placedCount;
    [SerializeField] private GameObject structure;
    [SerializeField] private GameObject piecesAnchor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer weldLines;

    void Awake()
    {
        buildOrder = new BuildOrder{type = variant, building = this, piecesList = this.piecesList, positionList = this.positionList};
        //healthManager = gameObject.GetComponent<HealthManager>();
        SetSelectedVisible(false);
        SetHealthVisible(false);
        
    }

    public void UpdateWeld(){
        weldAlpha = weldTime / totalWeldTime; 
        weldLines.color = new Color(255, 255, 255, weldAlpha);
    }

    IEnumerator CoolWeld(){
        float totalWeldTime = 3;
        for (float timeElapsed = totalWeldTime; timeElapsed > 0; timeElapsed -= Time.deltaTime)
        {
            float alpha = Mathf.InverseLerp(0, totalWeldTime, timeElapsed);
            weldLines.color = new Color(255, 255, 255, alpha);
            
            yield return null;
        }

        weldLines.color = new Color(255, 255, 255, 0);
        yield break;
    }

    void FixedUpdate(){

    }

    public void PlacePart(){
        this.healthManager.health += this.healthManager.maxHealth / partCount;
        piecesList.RemoveAt(0);
        positionList.RemoveAt(0);
        placedCount++;
        if(placedCount == partCount){
            built = true;
            for(int i = partCount - 1; i >= 0; i--){
                piecesAnchor.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                if(piecesAnchor.transform.GetChild(i).GetChild(0).childCount > 0){
                    piecesAnchor.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                }
            }
        }
    }

    public void Weld(){
        spriteRenderer.enabled = !spriteRenderer.enabled;
        built = false;
        structure.SetActive(true);
        piecesAnchor.SetActive(false);
        StartCoroutine(CoolWeld());
    }
}
