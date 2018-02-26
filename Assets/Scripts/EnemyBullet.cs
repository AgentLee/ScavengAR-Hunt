using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour 
{
	float speed;
	Rigidbody rb;
	Transform bullet;
	Collider collider;

	public GameObject shooter;

	bool hit;

	float destroyTime;

	// Use this for initialization
	void Start () 
	{
		Physics.IgnoreCollision(shooter.GetComponent<Collider>(), GetComponent<Collider>(), true);

		speed = 500.0f;

		bullet = GetComponent<Transform>();

		// rb = GetComponent<Rigidbody>();
		// rb.AddForce(bullet.up * speed);

		Destroy(gameObject, 5.0f);
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		
	}
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Ground") {
			// Debug.Log("HIT");
			// rb.useGravity = true;
			// rb.velocity = Vector3.zero;
			// rb.angularVelocity = Vector3.zero;
			Destroy(gameObject);
		}	
	}
}
