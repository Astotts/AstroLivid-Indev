using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorInterface : MonoBehaviour
{

    //Build UI
    [Header("Build UI")]
    private Vector3 mousePos;

    [SerializeField] private Mothership mothership;

    [SerializeField] List<GameObject> buildingList;
    [SerializeField] List<Sprite> buildngOutlineUI;
    BuildingIdentifyer building;
    private int buildIndex;

    //[SerializeField] private ResourceManager ResourceManager;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Color green;

    [SerializeField] private Color red;

    private Collider2D[] asteroidArray;

    private Collider2D[] buildingCheckArray;

    [SerializeField] private Collider2D buildOutlineCollider;
    [SerializeField] private GameObject buildOutline;

    [SerializeField] private ContactFilter2D contactFilter;
    private List<Collider2D> results;

    private bool spaceAvailable;

    //Action Interface
    [Header("Action Interface")]
    Texture2D[][] cursorTextureArray = new Texture2D[6][];
    [SerializeField] Texture2D[] moveIcon;
    [SerializeField] Texture2D[] patrolIcon;
    [SerializeField] Texture2D[] guardIcon;
    [SerializeField] Texture2D[] assistIcon;
    [SerializeField] Texture2D[] attackIcon;
    [SerializeField] Texture2D[] directFireIcon;
    private int iconNum; 
    private Vector2 hotspot;

    private bool anim;

    int iterator = 0;

    float elapsed;
    float duration;
    int length;

    void Start(){
        results = new List<Collider2D>();
        cursorTextureArray[0] = moveIcon;
        cursorTextureArray[1] = patrolIcon;
        cursorTextureArray[2] = guardIcon;
        cursorTextureArray[3] = assistIcon;
        cursorTextureArray[4] = attackIcon;
        cursorTextureArray[5] = directFireIcon;
        Cursor.SetCursor(cursorTextureArray[0][0], new Vector2(cursorTextureArray[0][0].width / 2, cursorTextureArray[0][0].height / 2), CursorMode.ForceSoftware);
    }

    public void SelectBuilding(int buildIndex_){
        buildIndex = buildIndex_;
        buildOutline.SetActive(true);
        spriteRenderer.sprite = buildingList[buildIndex].GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        //Building UI
        if(buildOutline.activeInHierarchy){
            //TODO Cursor.SetCursor(cursorTextureArray[iconNum][iterator], new Vector2(cursorTextureArray[iconNum][iterator].width / 2, cursorTextureArray[iconNum][iterator].height / 2), CursorMode.ForceSoftware);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(1)){
                buildOutline.SetActive(false);
            }

            buildOutline.transform.position = mousePos;

            if(buildOutlineCollider.OverlapCollider(contactFilter, results) > 0){
                spriteRenderer.color = red;
                spaceAvailable = false;
            }
            else{
                spaceAvailable = true;
                spriteRenderer.color = green;
            }

            building = buildingList[buildIndex].GetComponent<BuildingIdentifyer>();
            if(Input.GetMouseButtonDown(0) && spaceAvailable && ResourceManager.credits >= building.creditCost && ResourceManager.population >= building.populationCost){
                mothership.BuildStructure(buildIndex, buildOutline.transform.position, buildOutline.transform.rotation);
                if(!Input.GetButton("Shift")){
                    buildOutline.SetActive(false);
                }
            }
        }
        //Action Cursors
        elapsed += Time.deltaTime;
        if(anim){
            if(elapsed >= 0.07f && length - 1 > iterator){
                elapsed = 0;
                iterator++;
                Cursor.SetCursor(cursorTextureArray[iconNum][iterator], new Vector2(cursorTextureArray[iconNum][iterator].width / 2, cursorTextureArray[iconNum][iterator].height / 2), CursorMode.ForceSoftware);
            }
            if(elapsed >= 0.07f && length - 1 == iterator){
                anim = false;
                iterator = 0;
                Cursor.SetCursor(cursorTextureArray[iconNum][iterator], new Vector2(cursorTextureArray[iconNum][iterator].width / 2, cursorTextureArray[iconNum][iterator].height / 2), CursorMode.ForceSoftware);
                elapsed = 0;
            } 
        }
    
        if(Input.GetMouseButtonDown(1)){
            length = cursorTextureArray[iconNum].Length;
            anim = true;
        }
    }

    public void SetIcon(int num){
        iconNum = num;
        Cursor.SetCursor(cursorTextureArray[iconNum][0], new Vector2(cursorTextureArray[iconNum][0].width / 2, cursorTextureArray[iconNum][0].height / 2), CursorMode.ForceSoftware);
    }
}