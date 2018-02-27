using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour 
{
	private Transform enemy;
	private Rigidbody rb;

	public bool grounded;
	public float speed;

	float fireTime;
	float fireRate;

	public SimplePlayerController player;
	public GameObject target;
	public GameObject bullet;

	public float accuracy;
	public float shotEpsilon;
	public int shotFOV;

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();

		// player = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayerController>();

		speed = 0.25f;
		grounded = true;

		fireRate = 3;

		shotEpsilon = 100;
		accuracy = 0;
		shotFOV = 45;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fireTime >= fireRate) {
			Fire();
			fireTime = 0;
		}
		else {
			fireTime += Time.deltaTime;
		}

		if(enemy.position.y < -25.0f) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		transform.parent = null;

		if(collisionInfo.collider.tag == "Bullet" || transform.parent == null) {
			rb.useGravity = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		else if(collisionInfo.collider.tag == "Ground") {
			grounded = true;
		}
	}

	bool canShoot(out Vector3 shotDir)
	{
		// https://answers.unity.com/questions/321323/how-to-give-your-enemy-gun-inaccuracy.html
		// float myIntx = (float)Random.Range(-accuracy,accuracy)/1000;
		// float myInty = (float)Random.Range(-accuracy,accuracy)/1000;
		// float myIntz = (float)Random.Range(-accuracy,accuracy)/1000;
		// Vector3 newVector = new Vector3(transform.forward.x + myIntx, transform.forward.y + myInty, transform.forward.z + myIntz);

		// https://answers.unity.com/questions/180605/ai-enemy-shoot-precision.html
		// shotEpsilon = 0 --> 100% accuracy
		shotDir = target.transform.position + Random.insideUnitSphere * shotEpsilon - transform.position;  
		// Makes no sense to have the enemy shoot from behind, clamp to positive x
		shotDir.x = Mathf.Abs(shotDir.x);
		
		// Check to see the shot is within a reasonable shot range
		float dot = Vector3.Dot(transform.forward, shotDir);
		int theta = Mathf.RoundToInt(Mathf.Acos(dot) * 180 / Mathf.PI);

		return theta <= shotFOV;
	}

	void Fire()
	{
		Vector3 shotDir = Vector3.zero;
		if(canShoot(out shotDir)) {
			GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
			spawnedBullet.GetComponent<SimpleEnemyBulletController>().shooter = gameObject;
			spawnedBullet.GetComponent<Rigidbody>().AddForce(shotDir * 500.0f);
			Physics.IgnoreCollision(GetComponent<Collider>(), spawnedBullet.GetComponent<Collider>(), true);
		}

		Debug.DrawRay(transform.position, transform.forward * 10000F, Color.cyan, 2);
		Debug.DrawRay(transform.position, shotDir * 10000F, Color.red, 2);
		// Debug.DrawRay(transform.position, newVector * 1000F, Color.red, 2);  
	}
}
