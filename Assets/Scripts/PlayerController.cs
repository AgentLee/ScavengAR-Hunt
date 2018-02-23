using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Transform player;
	public GameObject bullet;
	float minBounds, maxBounds;
	float speed;

	public Joystick joystick;

	// Use this for initialization
	void Start () 
	{
		player = GetComponent<Transform>();
		speed = .15f;
		minBounds = -4f;
		maxBounds = 4f;
	}

	public void MovePlayer()
	{
		float h = Input.GetAxis ("Horizontal");

		if(joystick.InputDirection != Vector3.zero) {
			h = joystick.InputDirection.x;
		}

		// Stops player from going out of bound
		if (player.position.x < minBounds && h < 0) {
			h = 0;
		} 
		else if (player.position.x > maxBounds && h > 0) {
			h = 0;
		}

		player.position += Vector3.right * h * speed;
	}

	public void Fire()
	{
		if(Input.GetKeyDown(KeyCode.Space)) {
			GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
			// Ignore collisions from bullet at launch
			Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}

	public void FireTouch()
	{
		GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
		// Ignore collisions from bullet at launch
		Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>());
	}

	void FixedUpdate()
	{
		MovePlayer();
		Fire();
	}
}
