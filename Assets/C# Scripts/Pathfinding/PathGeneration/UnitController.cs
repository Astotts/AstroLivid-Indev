using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public GridController gridController;
    public GameObject unitPrefab;
    public int numUnitsPerSpawn;
    public float moveSpeed;

    [SerializeField] List<GameObject> unitsInGame;

	private void Awake()
	{
	}

	void Update()
	{
	}

	/*private void FixedUpdate()
	{
		if (gridController.curFlowField == null) { return; }
		foreach (GameObject unit in unitsInGame)
		{
			Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
			Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y, 0);
			Rigidbody2D unitRB = unit.GetComponent<Rigidbody2D>();
			unitRB.velocity = moveDirection * moveSpeed;
			/*To Do
			Research steering behaviors and smooth movement for AI;
			*/
	//	}
	//}
}

