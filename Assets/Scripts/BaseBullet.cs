/*
 * Bullet and EnemyBullet inherit from this class. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour 
{
	public Transform bullet;
	public float speed;
	
	// Min/max bounds for the bullet
	public float minY, maxY;

	// Moves the bullet based on the speed of the child class.
	public void MoveBullet(string firingFrom)
	{
		bullet.position += Vector3.up * speed;

		// Destroy the bullet if it goes below the plane.
		if(firingFrom == "Enemy") {
			if(bullet.position.y < minY) {
				Destroy(gameObject);
			}
		}
		// Destroy the bullet if it goes way too high.
		else {
			if(bullet.position.y > maxY) {
				Destroy(gameObject);
			}
		}
	}

	// Resets the speed to allow for gravity to take course.
	public void resetSpeed()
	{
		speed = 0;
	}
}
