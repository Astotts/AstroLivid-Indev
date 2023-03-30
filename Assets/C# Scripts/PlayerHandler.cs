using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Have the player assign their team
    void Start()
    {
        gameObject.tag = "TeamOne";
    }
}
