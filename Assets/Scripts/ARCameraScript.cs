using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraScript : MonoBehaviour 
{
	GameObject player, ground, bases, drones;
	Gyroscope gyro;

	// x, y, z
	float roll, pitch, yaw;

	Quaternion prev;

	void Awake()
	{
		// roll = transform.rotation.eulerAngles.x;
		// pitch = transform.rotation.eulerAngles.y;
		// yaw = transform.rotation.eulerAngles.z;

		// // prev = transform.rotation;

		// player = transform.Find("Player").gameObject;
		// player.GetComponent<SimplePlayerController>().
		// ground = transform.Find("Ground").gameObject;
		// bases = transform.Find("Bases").gameObject;
		// drones = transform.Find("Drones").gameObject;
	}

	// Use this for initialization
	void Start () 
	{
		// player = transform.Find("Player").gameObject;
		// ground = transform.Find("Ground").gameObject;
		// bases = transform.Find("Bases").gameObject;
		// drones = transform.Find("Drones").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// bool DEBUG = true;
		// if(DEBUG) {
		// 	// Pitch should always stay the same. Only want to change Roll and Yaw.
		// 	roll = transform.rotation.eulerAngles.x;
		// 	yaw = transform.rotation.eulerAngles.z;
			
		// 	Quaternion rot = Quaternion.Euler(roll, pitch, yaw);

		// 	// Update player, ground, bases
		// 	player.transform.rotation = prev;
		// 	ground.transform.rotation = prev;
		// 	bases.transform.rotation = prev;
		// 	drones.transform.rotation = rot;
		// }
	}
}
