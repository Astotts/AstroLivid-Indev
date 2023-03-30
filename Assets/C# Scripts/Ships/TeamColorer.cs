using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamColorer : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        tag = gameObject.tag;

        if(tag == "TeamOne"){    
            spriteRenderer.color = Color.red;
        }
        if(tag == "TeamTwo"){    
            spriteRenderer.color = Color.green;
        }
        if(tag == "TeamThree"){    
            spriteRenderer.color = Color.blue;
        }
        if(tag == "TeamFour"){    
            spriteRenderer.color = Color.cyan;
        }
        if(tag == "TeamFive"){    
            spriteRenderer.color = Color.gray;
        }
        if(tag == "TeamSix"){    
            spriteRenderer.color = Color.magenta;
        }
        if(tag == "TeamSeven"){    
            spriteRenderer.color = Color.white;
        }
        if(tag == "TeamEight"){    
            spriteRenderer.color = Color.yellow;
        }
        if(tag == "TeamNine"){    
            spriteRenderer.color = Color.black;
        }
    }
}
