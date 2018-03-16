using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemiesContainerController : MonoBehaviour 
{
	GameObject drones;
	float speed;
	float minBound, maxBound;

	// Use this for initialization
	void Start () 
	{	
		drones = gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// public void MoveDrones()
	// {
	// 	Debug.Log("move");
	// 	// drones.transform.LookAt(Camera.main.transform.position);

	// 	// Checks how many drones are left and increases speed as less are active
	// 	IncreaseDroneSpeed();

	// 	// Loop to actually move the drones left to right
	// 	drones.transform.position += Vector3.right * speed;
	// 	foreach(Transform drone in drones.transform) {
	// 		if(drone.position.x < minBound || drone.position.x > maxBound) {
	// 			droneSpeed = -droneSpeed;
	// 			moveDronesDown = true;
	// 			// drone.position -= new Vector3(0.0f, 2.0f, 0.0f);
	// 			return;
	// 		}
	// 	}

	// 	// If the drones reached the min/max bounds then the drones move down
	// 	if(moveDronesDown) {
	// 		foreach(Transform drone in drones.transform) {
	// 			drone.position -= new Vector3(0.0f, 0.75f, 0.0f);			
	// 		}

	// 		moveDronesDown = false;
	// 	}
	// }

	public void IncreaseDroneSpeed()
	{

	}
}
