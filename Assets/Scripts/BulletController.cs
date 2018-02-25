using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
	float speed;
	Rigidbody rb;
	Transform bullet;
	PlayerController player;
	// EnemyController enemy;
	public GameObject[] enemies;

	bool hit;

	float destroyTime;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		// enemy = GameObject.Find("Drone").GetComponent<EnemyController>();

		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		speed = 500f;
		
		destroyTime = 7;
		hit = false;

		rb = GetComponent<Rigidbody>();
		rb.AddForce(Camera.main.transform.forward * speed);

		// Need to tweak so that it will take longer to destroy after hitting an object
		Destroy(gameObject, destroyTime);
	}
	
	void FindClosestEnemy()
	{
		GameObject closestEnemy = null;
		float minDist = Mathf.Infinity;
		
		// Find the closest enemy by comparing the distance to the bullet.
		foreach(GameObject e in enemies) {
			float dist = Vector3.Distance(e.transform.position, this.transform.position);

			if(dist < minDist) {
				closestEnemy = e;
				minDist = dist;
			}
		}

		// If we found an enemy, have it dodge the bullet
		if(closestEnemy) {
			closestEnemy.GetComponent<EnemyController>().DodgeBullet(this.gameObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		FindClosestEnemy();
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
