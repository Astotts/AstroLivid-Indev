using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConstructionStates : ushort{
    Available = 0,
    Awaiting = 1,
    Grabbing = 2,
    Building = 3,
    Done = 4,
    Returning = 5,
}
