using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{

	private Transform enemy;
	public GameObject bullet;
	public float speed;
	public bool wasHit;
	public float lastShot;
	public float dT;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		speed = .05f;
		wasHit = false;
		lastShot = Time.deltaTime;
		dT = 1.25f;
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Bullet") {
			GetComponent<Rigidbody>().useGravity = true;
			wasHit = true;

			// Unparent 
			enemy.parent = null;

			Debug.Log("Turn on Gravity");
		}
	}

	public void MoveEnemyX(int direction)
	{
		enemy.Translate(direction * Vector3.right * Time.deltaTime * speed);
	}

	void FireBullet()
	{
		// if(Time.deltaTime - lastShot >= dT) {
		// 	GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
		// 	// Ignore collisions from bullet
		// 	Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		
		// } 
		// else {
		// 	lastShot = Time.deltaTime;			
		// }

		if(Random.value > 0.997f) {
			GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
			// Ignore collisions from bullet
			Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>());	
		}
	}

	void FixedUpdate () 
	{
		if(wasHit && enemy.position.y < -3f) {
			Destroy(gameObject);
		}

		FireBullet();
	}
}
