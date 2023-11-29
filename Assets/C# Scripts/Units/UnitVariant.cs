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
    ArtilleryCorvette = 9,
    ArtilleryFrigate = 10,
    ArtilleryDestroyer = 11,
    ArtilleryCapitalShip = 12,
    ArtilleryColonyship = 13,
    MiningFighter = 14,
    
    None = ushort.MaxValue
}
