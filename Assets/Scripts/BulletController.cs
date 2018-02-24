using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
	float speed;
	Rigidbody rb;
	Transform bullet;
	PlayerController player;
	EnemyController enemy;

	bool hit;

	float destroyTime;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		enemy = GameObject.Find("Drone").GetComponent<EnemyController>();

		speed = 500f;
		
		destroyTime = 7;
		hit = false;

		rb = GetComponent<Rigidbody>();
		rb.AddForce(Camera.main.transform.forward * speed);

		// Need to tweak so that it will take longer to destroy after hitting an object
		Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float distEnemyLeft = Vector3.Distance(this.transform.position, enemy.leftEye.transform.position);
		float distEnemyRight = Vector3.Distance(this.transform.position, enemy.rightEye.transform.position);

		if(!enemy.dodging && distEnemyLeft < 0.5 && distEnemyLeft < distEnemyRight) {
			enemy.DodgeBullet(-1);
			enemy.dodging = true;
		}
		else if(!enemy.dodging && distEnemyRight < 0.5 && distEnemyRight < distEnemyLeft) {
			enemy.DodgeBullet(1);
			enemy.dodging = true;
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
