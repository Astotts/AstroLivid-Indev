using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitVariant : ushort{
    //Buildings 0-7
    Mothership = 0,
    OreProcessor = 1,
    Citadel = 2,

    //Units 8-100
    ArtilleryFighter = 8,
    MiningFighter = 9,
    
    None = ushort.MaxValue
}
