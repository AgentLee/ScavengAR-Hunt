using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour 
{
	float speed;
	Rigidbody rb;
	Transform bullet;
	Collider collider;

	bool hit;

	float destroyTime;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

	}
	
	void OnCollisionEnter(Collision collisionInfo)
	{

	}
}
