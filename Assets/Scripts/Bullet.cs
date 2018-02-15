using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseBullet
{
	bool hitEnemy;

	// Use this for initialization
	void Start () 
	{
		bullet = GetComponent<Transform>();
		speed = .25f;
		maxY = 8f;
		hitEnemy = false;
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Enemy") {
			this.GetComponent<Rigidbody>().useGravity = true;
			resetSpeed();
		} 
	}

	void FixedUpdate()
	{
		// Check if bullet has been launched. Turn on collisions with player.
		if(bullet.position.y >= 0.512f) {
			Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), this.GetComponent<Collider>(), false);
		}

		// Moves bullet and desttroys the game object if out of bounds.
		MoveBullet("Player");
	}
}
