using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : BuildingIdentifyer
{   
    [SerializeField] private List<GameObject> buildingList;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BuildStructure(int index, Vector3 pos, Quaternion rot){

        GameObject building = Instantiate(buildingList[index], pos, rot);
        building.tag = this.gameObject.tag;
        building.layer = this.gameObject.layer;
    }
}