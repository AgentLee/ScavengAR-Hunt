using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraScript : MonoBehaviour 
{
	GameObject player, ground, bases;
	Gyroscope gyro;

	// x, y, z
	float _roll, _pitch, _yaw;	// Prev
	float roll, pitch, yaw;		// Curr

	Quaternion prev;

	// Use this for initialization
	void Start () 
	{
		player = transform.Find("Player").gameObject;
		ground = transform.Find("Ground").gameObject;
		bases = transform.Find("Bases").gameObject;

		_roll 	= -999f;
		_pitch 	= -999f;
		_yaw 	= -999f;

		roll = transform.rotation.eulerAngles.x;
		pitch = transform.rotation.eulerAngles.y;
		yaw = transform.rotation.eulerAngles.z;

		gyro = Input.gyro;
		gyro.enabled = true;

		prev = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Quaternion curr = rhsToLhs(Input.gyro.attitude);

		// player.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
		// ground.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
		// bases.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);

		player.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
		ground.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
		bases.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
	}

	Quaternion rhsToLhs(Quaternion quat) 
	{
		return new Quaternion(quat.x, quat.y, -quat.z, -quat.w);
	}
}
