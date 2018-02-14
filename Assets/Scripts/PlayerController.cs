using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Transform player;
	public GameObject bullet;
	float minBounds, maxBounds;
	float speed;

	// Use this for initialization
	void Start () 
	{
		player = GetComponent<Transform>();
		speed = .15f;
		minBounds = -40f;
		maxBounds = 40f;
	}

	void MovePlayer()
	{
		float h = Input.GetAxis ("Horizontal");

		// Stops player from going off screen
		if (player.position.x < minBounds && h < 0) {
			h = 0;
		} 
		else if (player.position.x > maxBounds && h > 0) {
			h = 0;
		}

		player.position += Vector3.right * h * speed;
	}

	void Fire()
	{
		if(Input.GetKeyDown(KeyCode.Space)) {
			GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
			// Ignore collisions from bullet
			Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}

	void FixedUpdate()
	{
		MovePlayer();
		Fire();

		
	}
}
