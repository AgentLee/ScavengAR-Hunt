using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour 
{
	Transform player;
	public GameObject bullet;
	public Joystick joystick;	

	public float minBounds, maxBounds, speed;

	// Use this for initialization
	void Start () 
	{
		player = this.transform;

		minBounds = -5f;
		maxBounds = 5f;
		speed = 0.02f;
	}

	public void FireBullet()
	{
		GameObject spawnedBullet = Instantiate(bullet, player.transform.position, new Quaternion());
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

	// Update is called once per frame
	void Update () 
	{
		MovePlayer();	
	}
}
