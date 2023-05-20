using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConstructionStates : ushort{
    Available = 0,
    Awaiting = 1,
    Building = 2,
    Done = 3,
    Returning = 4,
}
