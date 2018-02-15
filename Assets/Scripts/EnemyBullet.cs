using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseBullet 
{
	bool grounded;

	// Use this for initialization
	void Start () 
	{
		bullet = GetComponent<Transform>();
		speed = -.025f;
		minY = -3f;
		grounded = false;
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Player" && !grounded) {
			Destroy(gameObject);
			// Destroy(collisionInfo.collider.gameObject);
		}
		else if(collisionInfo.collider.tag == "Ground") {
			this.GetComponent<Rigidbody>().useGravity = true;
			resetSpeed();
			grounded = true;
		}
	}

	void FixedUpdate()
	{
		// Check if a player hit the enemy. Turn on collisions between enemy bullets.
		if(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().wasHit) {
			Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>(), this.GetComponent<Collider>(), false);
		}

		// Moves bullet and desttroys the game object if out of bounds.
		MoveBullet("Enemy");
	}
}
