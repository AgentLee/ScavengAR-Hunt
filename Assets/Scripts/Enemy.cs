using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	private Transform enemy;
	public GameObject bullet;
	public float speed;
	public bool wasHit;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		speed = .05f;
		wasHit = false;
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Bullet") {
			GetComponent<Rigidbody>().useGravity = true;
			wasHit = true;

			// Unparent 
			enemy.parent = null;
		}
	}

	void FireBullet()
	{
		if(Random.value > 0.997f) {
			GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
			// Ignore collisions from bullet
			Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>());	
			Debug.Log("SPAWN");
		}
	}

	void FixedUpdate () 
	{
		// Destroy enemy if it falls off below the plane.
		if(wasHit && enemy.position.y < -3f) {
			Destroy(gameObject);
		}
		
		// Only fire a bullet if it wasn't hit by the player.
		if(!wasHit) {
			FireBullet();
		}
	}
}
