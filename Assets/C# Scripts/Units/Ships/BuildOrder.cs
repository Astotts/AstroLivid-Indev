using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildOrder 
{
    public UnitVariant type;
    public BuildingIdentifyer building;
    public List<GameObject> piecesList;
    public List<Transform> positionList;
};
