using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform bullet;

	float speed;

	// Use this for initialization
	void Start () {
		bullet = GetComponent<Transform>();
		speed = .25f;
	}

	void MoveBullet()
	{
		bullet.position += Vector3.up * speed;

		if(bullet.position.y > 8f) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.tag != "Player") {
			Debug.Log(collisionInfo.collider.tag);
		}
	}

	void FixedUpdate()
	{
		MoveBullet();
	}
}
