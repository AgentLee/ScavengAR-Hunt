﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
	float speed;
	Rigidbody rb;
	Transform bullet;
	PlayerController player;

	bool hit;

	float destroyTime;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		speed = 500f;
		
		destroyTime = 7;
		hit = false;

		rb = GetComponent<Rigidbody>();
		rb.AddForce(Camera.main.transform.forward * speed);

		// Need to tweak so that it will take longer to destroy after hitting an object
		Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Enemy") {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			hit = true;

			// Don't want to add to the score if the player already hit the drone.
			if(!collisionInfo.collider.GetComponent<EnemyController>().grounded) {
				++player.score;
			}
		}
	}
}
