using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRedUFOController : SimpleEnemy 
{
	public float moveTime;
	private float time;

	GameManager manager;

	// Use this for initialization
	void Start () 
	{
		GetStartPos();
		IgnoreEnemies();

		collider	= GetComponent<Collider>();
		rb 			= GetComponent<Rigidbody>();
		hit 		= false;
		grounded 	= false;
		speed 		= (enemy.position.x < 0) ? 0.0025f : -0.0025f;	// Handled in coroutine...might move this out
		moveTime	= Random.Range(6, 20);
		time 		= 0.0f;
		pointValue 	= AssignPointValue();

		manager = GameObject.Find("Manager").GetComponent<GameManager>();
		if(!manager) {
			Debug.Log("Couldn't find manager");
		}
	}
	
	int AssignPointValue()
	{
		// Might need a strong RNG than this
		switch(Random.Range(0, 4)) 
		{
			case 0:
				return 50;
			case 1:
				return 100;
			case 2:
				return 150;
			case 3:
				return 300;
			// Should never get to this.
			default:
				return 0;
		}
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
		// Don't want the UFO to be moving while paused.
		if(manager.paused || manager.showingInstructions) {
			return;
		}

		time += Time.deltaTime;
		if(time >= moveTime && (!grounded || !hit)) {
			StartCoroutine(Move());
		}
		// else {
		// 	gameObject.SetActive(false);
		// }

		if(enemy.position.y < -15) {
			Destroy(gameObject);
		}
	}

	IEnumerator Move()
	{
		if(hit || grounded) {
			yield break;
		}

		// gameObject.SetActive(true);
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
