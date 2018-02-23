using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour 
{
	public Transform player;
	public EnemyController enemy;

	public float speed = 50f;

	float radius, radiusSpeed, rotationSpeed;
	
	void Start()
	{
		enemy = this.GetComponent<EnemyController>();

		radius = Random.Range(1, 5);
		radiusSpeed = Random.Range(0.1f, 1.0f);
		rotationSpeed = Random.Range(-10f, 10f);
	}

	// Update is called once per frame
	void Update () 
	{
		// transform.Rotate(Vector3.up, speed * Time.deltaTime);
		if(!enemy.hit) {
			transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
		}
	}
}
