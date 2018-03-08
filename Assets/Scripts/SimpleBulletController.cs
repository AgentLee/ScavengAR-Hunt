using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBulletController : MonoBehaviour 
{
	Rigidbody rb;
	
	private bool grounded;

	public SimplePlayerController player;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();	

		grounded = false;

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayerController>();
		if(!player)
			Debug.Log("COULDNT FIND PLAYER");

		Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Want the bullet to be able to roll around and interact with the player.
		if(grounded) {
			Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Enemy") {
			rb.useGravity 		= true;
			rb.velocity 		= Vector3.zero;
			rb.angularVelocity 	= Vector3.zero;

			// Need to adjust the row distribution
			SimpleEnemyController enemy = collisionInfo.gameObject.GetComponent<SimpleEnemyController>();
			// Only want to give the player points if they hit the drone. 
			// If the drone collides with another drone, they're out of luck.
			if(!enemy.hit) {
				player.score += enemy.pointValue;
			}


			// BROKEN CODE
			// if(!collisionInfo.collider.GetComponent<SimpleEnemyController>().grounded && !collisionInfo.collider.GetComponent<SimpleEnemyController>().hit) {
			// 	Debug.Log(player.score);
			// 	++player.score;
			// 	// // Update personal high score and server score
			// 	// if(player.score > PlayerPrefs.GetInt("PlayerScore")) {
			// 	// 	PlayerPrefs.SetInt("PlayerScore", player.score);
			// 	// 	Debug.Log("NEW HIGH SCORE");
			// 	// }
			// }
			// collisionInfo.collider.GetComponent<SimpleEnemyController>().hit = true;
		}
		else if(collisionInfo.collider.tag == "RedUFO") {
			rb.useGravity 		= true;
			rb.velocity 		= Vector3.zero;
			rb.angularVelocity 	= Vector3.zero;

			SimpleRedUFOController enemy = collisionInfo.gameObject.GetComponent<SimpleRedUFOController>();
			// Only want to give the player points if they hit the drone. 
			// If the drone collides with another drone, they're out of luck.
			if(!enemy.hit) {
				player.score += enemy.pointValue;
			}
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}


	}
}
