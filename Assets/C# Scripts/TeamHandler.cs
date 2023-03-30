using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public void SetUpInstatiatedObject(GameObject unit, bool building){
        if(unit.transform.GetChild(0).GetComponent<SpriteRenderer>()){
            spriteRenderer = unit.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        else if(unit.transform.GetComponent<SpriteRenderer>()){
            spriteRenderer = unit.transform.GetComponent<SpriteRenderer>();
        }
    
        tag = unit.tag;
            if(building){
                unit.layer = 11; 
            }
            /*else if(unit.GetComponent<AsteroidTargetingSystem>()){
                unit.layer = 15;
            }*/ 
            else{
                unit.layer = 10;
            }

            if(tag == "TeamOne"){
                if(spriteRenderer){
                    //spriteRenderer.color = Color.red;        
                }
            }
            if(tag == "TeamTwo"){ 
                if(spriteRenderer){
                    //spriteRenderer.color = Color.green;
                }
            }
            if(tag == "TeamThree"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.blue;
                }
            }
            if(tag == "TeamFour"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.cyan;
                }
            }
            if(tag == "TeamFive"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.gray;
                }
            }
            if(tag == "TeamSix"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.magenta;
                }
            }
            if(tag == "TeamSeven"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.white;
                }
            }
            if(tag == "TeamEight"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.yellow;
                }
            }
            if(tag == "TeamNine"){    
                if(spriteRenderer){
                    //spriteRenderer.color = Color.black;
                }
            }
        }    
    }
