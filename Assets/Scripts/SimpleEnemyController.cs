using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour 
{
	private Transform enemy;
	private Rigidbody rb;

	public bool grounded;
	public float speed;

	public SimplePlayerController player;

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayerController>();

		speed = 0.25f;
		grounded = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(enemy.position.y < -25.0f) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		transform.parent = null;

		if(collisionInfo.collider.tag == "Bullet" || transform.parent == null) {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}
	}
}
