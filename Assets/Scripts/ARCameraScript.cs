using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraScript : MonoBehaviour 
{
	GameObject player, ground, bases;

	// x, y, z
	float _roll, _pitch, _yaw;	// Prev
	float roll, pitch, yaw;		// Curr

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if(_roll )
	}
}
