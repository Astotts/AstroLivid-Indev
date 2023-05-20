using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildOrder 
{
    public UnitType.UnitVariant type;
    public List<GameObject> piecesList;
    public List<Transform> positionList;
};
