using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersManager : MonoBehaviour {

	public GameObject enemies;

	bool moveLeft = false;
	bool moveRight = true;
	float speed = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Moves the enemies left/right. 
	// If they reach the end bound they move down and gain speed.
	void MoveEnemies()
	{
		bool moveDown = false;
		foreach(Transform child in enemies.transform) {
			Enemy enemy = child.gameObject.GetComponent<Enemy>();

			enemy.transform.Translate(Vector3.right * speed);

			// Unparent if too far
			if(enemy.transform.position.x <= -10f || enemy.transform.position.x >= 10f) {
				enemy.transform.parent = null;
				continue;
			}

			// Change directions at a ceratain point
			if(enemy.transform.position.x <= -8f || enemy.transform.position.x >= 8f) {
				speed = -speed;
				moveDown = true;
			}
		}

		if(moveDown) {
			foreach(Transform child in enemies.transform) {
				Enemy enemy = child.gameObject.GetComponent<Enemy>();

				enemy.transform.Translate(new Vector3(0, -.35f, 0));
				speed *= 1.25f;	
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		MoveEnemies();
	}
}
