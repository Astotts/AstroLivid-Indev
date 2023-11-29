using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Vector3 destination;
    [SerializeField] ShipIdentifyer ship;
    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector3(773f,Random.Range(150f,330f),0f);
        ship.MoveTo(destination);
    }
}
