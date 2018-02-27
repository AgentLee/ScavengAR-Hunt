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
		if(grounded) {
			Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Enemy") {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			++player.score;

			if(!collisionInfo.collider.GetComponent<SimpleEnemyController>().grounded) {
				Debug.Log(player.score);
				++player.score;
			}
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}
	}
}
