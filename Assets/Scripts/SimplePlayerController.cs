﻿using System.Collections;
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

	private bool hit;
	public int numLives;

	// Use this for initialization
	void Start () 
	{
		player = this.transform;
		score = 0;

		speed = 0.5f;
		minBounds = -12.0f;
		maxBounds = 12.0f;

		rb = GetComponent<Rigidbody>();
		// rb.isKinematic = false;
		// rb.useGravity = true;

		hit = false;
		numLives = 5;
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
		// if(collisionInfo.collider.tag == "Ground") {
		// 	rb.isKinematic = true;
		// } 
		if(collisionInfo.collider.tag == "Enemy Bullet") {
			if(!hit && !collisionInfo.collider.GetComponent<SimpleEnemyBulletController>().grounded) {
				// Make sure there are enough lives for the player to blink
				if(--numLives <= 0) {
					Destroy(gameObject);
					UpdateScores();
				}
				else {
					StartCoroutine(Blink());
				}
			}
		}
	}

	IEnumerator Blink()
	{
		hit = true;

		for(int blink = 0; blink < 5; ++blink) {
			yield return new WaitForSeconds(.05f);
			GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled; 
			yield return new WaitForSeconds(.05f);
		}

		// Make sure the player is turned back on
		GetComponent<MeshRenderer>().enabled = true; 
		hit = false;
	}

	public void UpdateScores()
	{
		Debug.Log("Updating...");

		PlayerPrefs.SetInt("PlayerScore", score);

		string _name 	= PlayerPrefs.GetString("PlayerName");
		string _phone 	= PlayerPrefs.GetString("PlayerPhone");
		int _score 		= PlayerPrefs.GetInt("PlayerScore");
		string _email 	= PlayerPrefs.GetString("PlayerEmail");

		// Update the database
		GetComponent<HighScores>().AddHighScore(_name, _score, _phone, _email);
	}
}
