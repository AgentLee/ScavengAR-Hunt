using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRedUFOController : SimpleEnemy 
{
	public float moveTime;
	private float time;

	// Use this for initialization
	void Start () 
	{
		GetStartPos();
		IgnoreEnemies();

		collider	= GetComponent<Collider>();
		rb 			= GetComponent<Rigidbody>();
		hit 		= false;
		grounded 	= false;
		speed 		= (enemy.position.x < 0) ? 0.15f : -0.15f;
		moveTime	= Random.Range(6, 20);
		time 		= 0.0f;
	}
	
	void IgnoreEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < enemies.Length; ++i) {
			Physics.IgnoreCollision(GetComponent<Collider>(), enemies[i].GetComponent<Collider>(), true);
		}
	}

	void GetStartPos()
	{
		float x;
		switch(Random.Range(0, 2)) 
		{
			case 0:
				x = -30;
				break;
			case 1:
				x = 30;
				break;
			default:
				x = 30;
				break;	
		}

		enemy = transform;
		enemy.position = new Vector3(x, enemy.position.y, enemy.position.z);
	}

	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;
		if(time >= moveTime) {
			StartCoroutine(Move());
		}
	}

	IEnumerator Move()
	{
		if(speed < 0) {
			enemy.eulerAngles = new Vector3(0, -90, 0);
			while(enemy.position.x >= -30) {
				enemy.position += Vector3.right * speed;
				
				yield return null;
			}

			speed = -speed;
		}
		else if(speed > 0) {
			enemy.eulerAngles = new Vector3(0, 90, 0);
			while(enemy.position.x <= 30) {
				enemy.position += Vector3.right * speed;

				yield return null;
			}

			speed = -speed;
		}

		moveTime = Random.Range(5, 10);
		time = 0;
	}
}
