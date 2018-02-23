using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour 
{

	public GameObject enemies;

	float speed = 0.0025f;

	// Use this for initialization
	void Start () 
	{
		Physics.gravity = new Vector3(0, -3f, 0);		
	}
	
	// Moves the enemies left/right. 
	// If they reach the end bound they move down and gain speed.
	void MoveEnemies()
	{
		bool moveDown = false;
		foreach(Transform child in enemies.transform) {
			EnemyController2 enemy = child.gameObject.GetComponent<EnemyController2>();

			enemy.transform.Translate(Vector3.right * speed);

			// Unparent if too far
			if(enemy.transform.position.x <= -10f || enemy.transform.position.x >= 10f) {
				enemy.transform.parent = null;
				continue;
			}

			// Change directions at a ceratain point
			if(enemy.transform.position.x <= -2f || enemy.transform.position.x >= 2f) {
				speed = -speed;
				moveDown = true;
			}
		}

		if(moveDown) {
			foreach(Transform child in enemies.transform) {
				EnemyController2 enemy = child.gameObject.GetComponent<EnemyController2>();

				enemy.transform.Translate(new Vector3(0, -.06f, 0));
				speed *= 1.025f;	
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		MoveEnemies();
	}
}
