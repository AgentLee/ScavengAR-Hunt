using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour 
{
	private Transform player;
	private Rigidbody rb;

	public GameObject bullet;
	public int score;

	public Joystick joystick;

	public float speed;
	public float minBounds, maxBounds;

	// Use this for initialization
	void Start () 
	{
		player = this.transform;
		score = 0;

		speed = 0.5f;
		minBounds = -12.0f;
		maxBounds = 12.0f;

		rb = GetComponent<Rigidbody>();
		rb.isKinematic = false;
		rb.useGravity = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float h = joystick.InputDirection.x;

		if(player.position.x < minBounds && h < 0 ||
            player.position.x > maxBounds && h > 0)
			h = 0;
		
		player.position += Vector3.right * h * speed;
	}

	public void Fire()
	{
		GameObject spawnedBullet = Instantiate(bullet, player.position, new Quaternion());			
		Physics.IgnoreCollision(spawnedBullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
		spawnedBullet.GetComponent<Rigidbody>().AddForce(player.up * 500.0f);
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag == "Ground") {
			rb.isKinematic = true;
		} 
	}
}
