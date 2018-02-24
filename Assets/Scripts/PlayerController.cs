using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	private Transform player;
	public GameObject bullet;
	public int score;

	// Use this for initialization
	void Start () 
	{
		player = this.transform;
		score = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Vector3 fwd = player.TransformDirection(Vector3.forward);

		// RaycastHit hit;
		// Ray ray = new Ray(player.position, fwd);

		// Debug.DrawRay(ray.origin, ray.direction, Color.green);

		// if(Physics.Raycast(ray, out hit, 20)) {
		// 	if(hit.collider != null) {
		// 		Debug.Log(hit.collider.tag);
		// 	}
		// 	else {
		// 		Debug.Log("NO HIT");
		// 	}
		// }
	}

	public void Fire()
	{
		Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation * Quaternion.Euler(-90, 0, 0));
	}
}
