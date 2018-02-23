using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
	public float speed;
	PlayerController2 player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<PlayerController2>();
		speed = 50f;	
		// this.GetComponent<Rigidbody>().AddForce(player.transform.up * speed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Enemy") {
			this.GetComponent<Rigidbody>().useGravity = true;

			// Remove force from rigid body
			this.GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		} 
	}
}
