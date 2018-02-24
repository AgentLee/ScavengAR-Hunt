using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public GameObject player;

	Rigidbody rb;

	public bool hit;

	public bool grounded;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();

		Vector3 lookAt = player.transform.position - transform.position;
		lookAt.y = 0;

		Quaternion rotation = Quaternion.LookRotation(lookAt);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

		hit = false;
		grounded = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Bullet") {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			
			// Unparent to stop moving
			transform.parent = null;
			hit = true;
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}
	}
}
