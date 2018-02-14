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
	
	void MoveEnemies()
	{
		bool moveDown = false;
		enemies.transform.Translate(Vector3.right * speed);
		
		foreach(Transform child in enemies.transform) {
			Enemy enemy = child.gameObject.GetComponent<Enemy>();

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
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		MoveEnemies();
	}
}
