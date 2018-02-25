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
	
	GameObject FindClosestEnemy()
	{
		GameObject tMin = null;
		float minDist = Mathf.Infinity;

		Vector3 bulletPos = transform.position;
		
		foreach(GameObject e in enemies) {
			float dist = Vector3.Distance(e.transform.position, bulletPos);

			if(dist < minDist) {
				tMin = e;
				minDist = dist;
			}
		}

		return tMin;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		// GameObject closestEnemy = null;
		// float distLeft = 0;
		// float distRight = 0;
		// foreach(GameObject e in enemies) {
		// 	EnemyController enemy = e.GetComponent<EnemyController>();
			
		// 	if(closestEnemy == null && e != null) {
		// 		closestEnemy = e;
		// 		distLeft = Vector3.Distance(this.transform.position, enemy.leftEye.transform.position);
		// 		distRight = Vector3.Distance(this.transform.position, enemy.rightEye.transform.position);
		// 		continue;
		// 	}

		// 	EnemyController closest = closestEnemy.GetComponent<EnemyController>();
			
		// 	float distEnemyLeft = Vector3.Distance(this.transform.position, enemy.leftEye.transform.position);
		// 	float distEnemyRight = Vector3.Distance(this.transform.position, enemy.rightEye.transform.position);
		// 	float closestEnemyLeft = Vector3.Distance(this.transform.position, closest.leftEye.transform.position);
		// 	float closestEnemyRight = Vector3.Distance(this.transform.position, closest.rightEye.transform.position);

		// 	if(distEnemyLeft > closestEnemyLeft || distEnemyRight > closestEnemyRight) {
		// 		continue;
		// 	} 
		// 	else {
		// 		closestEnemy = e;
		// 		distLeft = distEnemyLeft;
		// 		distRight = distEnemyRight;
		// 	}
		// }

		GameObject closestEnemy = FindClosestEnemy();
		if(closestEnemy != null) {
			EnemyController enemy = closestEnemy.GetComponent<EnemyController>();

			float distLeft = Vector3.Distance(this.transform.position, enemy.leftEye.transform.position);
			float distRight = Vector3.Distance(this.transform.position, enemy.rightEye.transform.position);

			if(!enemy.dodging && distLeft < 0.5 && distLeft < distRight) {
				enemy.DodgeBullet(-1);
				enemy.dodging = true;
			}
			else if(!enemy.dodging && distRight < 0.5 && distRight < distLeft) {
				enemy.DodgeBullet(1);
				enemy.dodging = true;
			}
		}

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
