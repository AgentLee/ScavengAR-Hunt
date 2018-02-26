using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour 
{
	public Transform spawnPoint;
	public GameObject bullet;

	// Use this for initialization
	void Start () 
	{
		// GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
		// spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10.0f);		
		// spawnedBullet.GetComponent<EnemyBullet>()
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space)) {
			GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
			spawnedBullet.GetComponent<EnemyBullet>().shooter = gameObject;
		}
	}
}
