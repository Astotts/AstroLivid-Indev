using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorInterface : MonoBehaviour
{
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
        cursorTextureArray[0] = moveIcon;
        cursorTextureArray[1] = patrolIcon;
        cursorTextureArray[2] = guardIcon;
        cursorTextureArray[3] = assistIcon;
        cursorTextureArray[4] = attackIcon;
        cursorTextureArray[5] = directFireIcon;
        Cursor.SetCursor(cursorTextureArray[0][0], new Vector2(cursorTextureArray[0][0].width / 2, cursorTextureArray[0][0].height / 2), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(anim){
            if(elapsed >= 0.07f && length - 1 > iterator){
                elapsed = 0;
                iterator++;
                Debug.Log(iterator);
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
