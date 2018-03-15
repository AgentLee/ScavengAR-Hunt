using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraScript : MonoBehaviour 
{
	GameObject player, ground, bases;
	Gyroscope gyro;

	// x, y, z
	float roll, pitch, yaw;

	Quaternion prev;
	bool DEBUG = false;

	// Use this for initialization
	void Start () 
	{
		player = transform.Find("Player").gameObject;
		ground = transform.Find("Ground").gameObject;
		bases = transform.Find("Bases").gameObject;

		roll = transform.rotation.eulerAngles.x;
		pitch = transform.rotation.eulerAngles.y;
		yaw = transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(DEBUG) {
			// Pitch should always stay the same. Only want to change Roll and Yaw.
			roll = transform.rotation.eulerAngles.x;
			yaw = transform.rotation.eulerAngles.z;
			
			Quaternion rot = Quaternion.Euler(roll, pitch, yaw);

			// Update player, ground, bases
			player.transform.rotation = rot;
			ground.transform.rotation = rot;
			bases.transform.rotation = rot;
		}
	}
}
