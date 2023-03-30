using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
	int count = -1;
    [SerializeField] private GridController gridController;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float roughTerrainMoveSpeed;

	private static List<AsteroidIdentifyer> asteroidIdentifyerList;

	private static AsteroidIdentifyer asteroidIdentifyer;

	public void Start(){
		asteroidIdentifyerList = new List<AsteroidIdentifyer>{};
	}
	
	public static void SetAsteroidList(Collider2D asteroid){
		bool asteroidFailed = false;
		foreach(AsteroidIdentifyer asteroidTest in asteroidIdentifyerList){
			if(asteroidTest == asteroid.GetComponent<AsteroidIdentifyer>()){
				asteroidFailed = true;
				//Debug.Log(asteroidFailed);
			}
		}
		if(asteroid && !asteroidFailed){
			//Debug.Log(asteroidFailed);
            asteroidIdentifyer = asteroid.GetComponent<AsteroidIdentifyer>();
			if(asteroidIdentifyer != null){
				asteroidIdentifyerList.Add(asteroidIdentifyer);
			}
		}
	}
	
	private void FixedUpdate()
	{
		if(asteroidIdentifyerList != null && asteroidIdentifyerList.Count > 0){
			foreach(AsteroidIdentifyer asteroid in asteroidIdentifyerList){
				count++;
				//Debug.Log(asteroid + " " + count);
				if(asteroid != null && asteroid.curFlowField != null){
					//Debug.Log(asteroid.curFlowField.destinationCell.worldPos);
					Cell cellBelow = asteroid.curFlowField.GetCellFromWorldPos(asteroid.transform.position);
					Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y, 0f);
					Rigidbody2D asteroidRB = asteroid.GetComponent<Rigidbody2D>();
					asteroidRB.position += (Vector2)moveDirection * asteroid.moveSpeed * Time.deltaTime;
				}
			}
			count = -1;
		}
	}
}
