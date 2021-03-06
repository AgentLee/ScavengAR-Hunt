﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBulletController : MonoBehaviour 
{
	Rigidbody rb;
	
	public bool grounded;

	public GameObject shooter;
	public GameObject playerBase;

	public AudioSource thudSound;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();	
		playerBase = GameObject.FindGameObjectWithTag("Shield");
		if(!playerBase) {
			Debug.Log("Couldn't find base");
		}

		grounded = false;

		Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>(), true);
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Shield" || collisionInfo.collider.tag == "Player") {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		else if(collisionInfo.collider.tag == "Ground") {
			if(!grounded) {
				thudSound.Play();
			}
			
			grounded = true;
		}
	}
}
